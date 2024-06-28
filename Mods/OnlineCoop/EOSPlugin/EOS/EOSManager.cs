using Epic.OnlineServices.Platform;
using System.IO;
using Epic.OnlineServices.IntegratedPlatform;
using Epic.OnlineServices.Lobby;
using System.Reflection;
using BepInEx.Logging;
using System.Linq;
using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.Sessions;
using EOSPlugin.Utility;
using Steamworks;

namespace EOSPlugin.EOS
{
    internal static class EOSManager
    {
        internal static PlatformInterface platformInterface;

        internal static AuthInterface authInterface;
        internal static ConnectInterface connectInterface;
        internal static IntegratedPlatformOptionsContainer integratedPlatformOptionsContainer;
        internal static LobbyInterface lobbyInterface;
        internal static P2PInterface p2pInterface;
        internal static SessionsInterface sessionInterface;
        private static EpicAccountId epicId;
        private static ProductUserId productUserId;
        
        private static Utf8String LobbyId;
        private static Utf8String BucketId;

        internal static bool InLoby => LobbyId != null;

        internal static ManualLogSource Logger => EOSPlugin.logSource;


        public static DateTime lastTick = DateTime.Now;
        internal  static void Tick()
        {
            if (platformInterface != null && DateTime.Now.Subtract(lastTick).TotalMilliseconds > 100)
            {
                platformInterface?.Tick();
                lastTick = DateTime.Now;
            }

            if (p2pInterface != null)
            {
                var rpo = new ReceivePacketOptions
                {
                    LocalUserId = productUserId,
                    MaxDataSizeBytes = 1170,
                };
                ArraySegment<byte> Data = new ArraySegment<byte>();
                p2pInterface.ReceivePacket(ref rpo, out ProductUserId outPeerId, out SocketId outSocketId, out byte outChannel, Data, out uint outBytesWritten);
            }
        }

        internal static void Initialize()
        {
            var path = new[] { Environment.GetEnvironmentVariable("PATH") ?? string.Empty };

            // Combine the paths and remove duplicates
            string newPath = string.Join(Path.PathSeparator.ToString(), path.Concat(new[] { Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) }).Distinct());
            Environment.SetEnvironmentVariable("PATH", newPath);

            Utility.Settings.Initialize();

            var initializeOptions = new InitializeOptions()
            {
                ProductName = "Modded ",
                ProductVersion = "1.0.1"
            };

            Logger.LogDebug("Starting PlatformInterface Initialize");
            Result initializeResult = PlatformInterface.Initialize(ref initializeOptions);
            Logger.LogDebug($"Initialize");

            Logger.LogDebug($"Creating IPOC Options");
            var createIntegratedPlatformOptionsContainerOptions = new CreateIntegratedPlatformOptionsContainerOptions();
            integratedPlatformOptionsContainer = null;
            IntegratedPlatformInterface.CreateIntegratedPlatformOptionsContainer(ref createIntegratedPlatformOptionsContainerOptions, out integratedPlatformOptionsContainer);

            var options = new WindowsOptions()
            {
                ProductId = Settings.ProductId,
                SandboxId = Settings.SandboxId,
                ClientCredentials = new ClientCredentials()
                {
                    ClientId = Settings.ClientId,
                    ClientSecret = Settings.ClientSecret
                },
                DeploymentId = Settings.DeploymentId,
                Flags = PlatformFlags.None,
                IsServer = true,
                IntegratedPlatformOptionsContainerHandle = integratedPlatformOptionsContainer
            };

            Logger.LogDebug($"Creating Platform Interface from Options");
            platformInterface = PlatformInterface.Create(ref options);

            if (platformInterface == null)
            {
                Logger.LogWarning($"Failed to create platform. Ensure the relevant {typeof(Settings)} are set or passed into the application as arguments.");
                return;
            }

            if (integratedPlatformOptionsContainer != null)
            {
                integratedPlatformOptionsContainer.Release();
                integratedPlatformOptionsContainer = null;
            }

            // We connected, now get all the interfaces and log in
            GetAllInterfaces();
            // Login();            
        }

        private static void GetAllInterfaces(){
            authInterface = platformInterface.GetAuthInterface();
            connectInterface = platformInterface.GetConnectInterface();
            lobbyInterface = platformInterface.GetLobbyInterface();
            p2pInterface = platformInterface.GetP2PInterface();
            sessionInterface = platformInterface.GetSessionsInterface();
        }

        // Login to the EOS SDK
        private static void Login()
        {
            var loginOptions = new Epic.OnlineServices.Auth.LoginOptions()
            {
                Credentials = new Epic.OnlineServices.Auth.Credentials()
                {
                    Type = Settings.LoginCredentialType,
                    Id = Settings.Id,
                    Token = Settings.Token,
                },
                ScopeFlags = AuthScopeFlags.Presence | AuthScopeFlags.FriendsList | AuthScopeFlags.BasicProfile,
            };

            authInterface.Login(ref loginOptions, null, (ref Epic.OnlineServices.Auth.LoginCallbackInfo loginCallbackInfo) =>
            {
                if (loginCallbackInfo.ResultCode == Result.Success)
                {
                    Logger.LogDebug("Login succeeded");

                    epicId = authInterface.GetLoggedInAccountByIndex(0);
                    if (epicId.IsValid())
                    {
                        Logger.LogDebug($"Logged in as {epicId}");
                    }
                    else
                    {
                        Logger.LogDebug("Failed to get logged in account id");
                        return;
                    }

                    Epic.OnlineServices.Auth.CopyIdTokenOptions copyIdTokenOptions = new Epic.OnlineServices.Auth.CopyIdTokenOptions
                    {
                        AccountId = epicId,
                    };
                    authInterface.CopyIdToken(ref copyIdTokenOptions, out Epic.OnlineServices.Auth.IdToken? outIdToken);

                    Epic.OnlineServices.Connect.LoginOptions loginOptions = new Epic.OnlineServices.Connect.LoginOptions()
                    {
                        Credentials = new Epic.OnlineServices.Connect.Credentials()
                        {
                            Type = ExternalCredentialType.EpicIdToken,
                            Token = outIdToken.Value.JsonWebToken,
                        },
                    };

                    // Login with Connect (not Auth)
                    connectInterface.Login(ref loginOptions, null, (ref Epic.OnlineServices.Connect.LoginCallbackInfo info) => {
                        Logger.LogDebug($"Connect Login {info.ResultCode}: {info.LocalUserId}");
                        productUserId = info.LocalUserId;
                    });
                }
                else if (Common.IsOperationComplete(loginCallbackInfo.ResultCode))
                {
                    Logger.LogDebug("Login failed: " + loginCallbackInfo.ResultCode);
                    if (Settings.LoginCredentialType != LoginCredentialType.AccountPortal)
                    {
                        Logger.LogDebug("Using Portal ");
                        Settings.LoginCredentialType = LoginCredentialType.AccountPortal;
                        Login();
                    }      
                }
            });
        }

        // Create a lobby for the campaign if one does not exist
        private static void CreateLobby()
        {
            string campaignId = "Temp";

            Logger.LogDebug($"Creating lobby Options");
            BucketId = $"Campaign";
            var clo = new CreateLobbyOptions()
            {
                PermissionLevel = LobbyPermissionLevel.Publicadvertised,
                MaxLobbyMembers = 32,
                BucketId = BucketId,
                LocalUserId = productUserId,
                DisableHostMigration = false,
            };

            // Create said lobby and join it
            Logger.LogDebug($"Creating lobby");
            lobbyInterface.CreateLobby(ref clo, null, (ref CreateLobbyCallbackInfo lobbyCallbackInfo) =>
            {
                if (lobbyCallbackInfo.ResultCode == Result.Success)
                {
                    Logger.LogDebug($"Created Lobby {lobbyCallbackInfo.LobbyId}");
                    LobbyId = lobbyCallbackInfo.LobbyId;

                    var ulmo = new UpdateLobbyModificationOptions()
                    {
                        LobbyId = LobbyId,
                        LocalUserId = productUserId,
                    };

                    lobbyInterface.UpdateLobbyModification(ref ulmo, out LobbyModification outLobbyModificationHandle);
                    
                    if (outLobbyModificationHandle == null)
                    {
                        Logger.LogDebug("Failed to create lobby modification handle");
                        return;
                    }

                    Logger.LogDebug("Adding CampaignId to Lobby");
                    var lmaao = new LobbyModificationAddAttributeOptions()
                    {
                        Attribute = new Epic.OnlineServices.Lobby.AttributeData()
                        {
                            Key = "CampaignId",
                            Value = campaignId,
                        },
                    };
                    var result = outLobbyModificationHandle.AddAttribute(ref lmaao);
                    Logger.LogDebug($"Added CampaignId to Lobby: {result}");

                    // Add the sockets and get ready for people to join
                    AddSockets();

                    // Make the lobby searchable
                    var ulo = new UpdateLobbyOptions()
                    {
                        LobbyModificationHandle = outLobbyModificationHandle,
                    };

                    lobbyInterface.UpdateLobby(ref ulo, null, (ref UpdateLobbyCallbackInfo updateLobbyCallbackInfo) =>
                    {
                        outLobbyModificationHandle.Release();
                        if (updateLobbyCallbackInfo.ResultCode == Result.Success)
                        {
                            Logger.LogDebug($"Updated Lobby {updateLobbyCallbackInfo.LobbyId}");
                        }
                        else if (Common.IsOperationComplete(updateLobbyCallbackInfo.ResultCode))
                        {
                            Logger.LogDebug("Update Lobby failed: " + updateLobbyCallbackInfo.ResultCode);
                        }
                    });
                }
                else if (Common.IsOperationComplete(lobbyCallbackInfo.ResultCode))
                {
                    Logger.LogDebug("Create Lobby failed: " + lobbyCallbackInfo.ResultCode);
                }
            });
        }

        internal static void JoinCampaignLobby()
        {
            Logger.LogDebug("Joining Campaign Lobby");
            string campaignId = "Temp";

            Logger.LogDebug($"Creating Lobby Search Option");
            var clso = new CreateLobbySearchOptions()
            {
                MaxResults = 100,
            };
            Logger.LogDebug($"Creating Lobby Search");
            lobbyInterface.CreateLobbySearch(ref clso, out var lobbySearchHandle);

            if (lobbySearchHandle == null)
            {
                Logger.LogDebug("Failed to create lobby search handle");
                return;
            }

            Logger.LogDebug($"Creating Lobby Search Parameters");
            var lspo = new LobbySearchSetParameterOptions()
            {
                Parameter = new Epic.OnlineServices.Lobby.AttributeData()
                {
                    Key = "CampaignId",
                    Value = campaignId, //campaignId,
                },
            };

            lobbySearchHandle.SetParameter(ref lspo);

            Logger.LogDebug($"Creating Lobby Search Find Option");
            var lsfo = new LobbySearchFindOptions()
            {
                LocalUserId = productUserId,
            };

            Logger.LogDebug($"Trigger Lobby Search Find");
            lobbySearchHandle.Find(ref lsfo, null, (ref LobbySearchFindCallbackInfo lobbySearchFindCallbackInfo) =>
            {
                if (lobbySearchFindCallbackInfo.ResultCode == Result.Success)
                {
                    Logger.LogDebug($"Creating Count Request");
                    var countRequest = new LobbySearchGetSearchResultCountOptions();

                    Logger.LogDebug($"Request Count");
                    uint count = lobbySearchHandle.GetSearchResultCount(ref countRequest);

                    Logger.LogDebug($"Counted {count} open lobbies");
                    for (uint i = 0; i < count; i++)
                    {
                        Logger.LogDebug($"Creating Lobby Search Copy result options {i}");
                        var options = new LobbySearchCopySearchResultByIndexOptions()
                        {
                            LobbyIndex = i,
                        };

                        Logger.LogDebug($"Searching by options");
                        lobbySearchHandle.CopySearchResultByIndex(ref options, out var lobbySearchResult);

                        Logger.LogDebug($"Creating copy info option");
                        LobbyDetailsCopyInfoOptions copyInfoOptions = new LobbyDetailsCopyInfoOptions();
                        Logger.LogDebug($"Using copy info option");
                        lobbySearchResult.CopyInfo(ref copyInfoOptions, out var lobbyInfo);

                        Logger.LogDebug($"Creating Lobby Search");
                        if (lobbyInfo.HasValue)
                            Logger.LogDebug($"Found Lobby {lobbyInfo.Value.LobbyId}");
                        else
                            Logger.LogDebug($"No Lobby Found for id {i}");

                        var jlbio = new JoinLobbyByIdOptions()
                        {
                            LobbyId = lobbyInfo.Value.LobbyId,
                            LocalUserId = productUserId,
                        };
                        lobbyInterface.JoinLobbyById(ref jlbio, null, (ref JoinLobbyByIdCallbackInfo data) => { 
                            if (data.ResultCode == Result.Success)
                            {
                                LobbyId = data.LobbyId;
                                Logger.LogDebug($"Joined Lobby {data.LobbyId}");

                                AddSockets();
                            }
                            else if (Common.IsOperationComplete(data.ResultCode))
                            {
                                Logger.LogDebug("Join Lobby failed: " + data.ResultCode);
                            }
                        });
                    }
                    if (count == 0)
                    {
                        Logger.LogDebug("No Lobbies Found");
                        CreateLobby();
                    }

                }
                else if (Common.IsOperationComplete(lobbySearchFindCallbackInfo.ResultCode))
                {
                    Logger.LogDebug("Search Lobby failed: " + lobbySearchFindCallbackInfo.ResultCode);
                }
            });
        }

        private static ulong NotifyJoinLobbyId;

        internal static void AddSockets()
        {
            var anjlao = new AddNotifyJoinLobbyAcceptedOptions();
            NotifyJoinLobbyId = lobbyInterface.AddNotifyJoinLobbyAccepted(ref anjlao, null, (ref JoinLobbyAcceptedCallbackInfo data) =>
            {
                Logger.LogDebug($"Joined Lobby {data.LocalUserId}");
                var aco = new AcceptConnectionOptions { 
                    LocalUserId = productUserId, 
                    RemoteUserId = data.LocalUserId,
                    SocketId = socketId,
                };
                p2pInterface.AcceptConnection(ref aco);
            });
        }

        internal static SocketId? socketId = new SocketId() { SocketName = "Socket"};

        internal static void LeaveCampaignLobby()
        {
            Logger.LogDebug("Leaving Campaign Lobby");
            var llo = new LeaveLobbyOptions()
            {
                LobbyId = LobbyId,
                LocalUserId = productUserId,
            };

            lobbyInterface.RemoveNotifyJoinLobbyAccepted(NotifyJoinLobbyId);

            lobbyInterface.LeaveLobby(ref llo, null, (ref LeaveLobbyCallbackInfo lobbyCallbackInfo) =>
            {
                if (lobbyCallbackInfo.ResultCode == Result.Success)
                    Logger.LogDebug($"Left Lobby {lobbyCallbackInfo.LobbyId}");
                else
                    Logger.LogDebug("Leave Lobby failed: " + lobbyCallbackInfo.ResultCode);
            });
            LobbyId = null;

            var cco = new CloseConnectionsOptions()
            {
                LocalUserId = productUserId,
                SocketId = socketId
            };
            p2pInterface.CloseConnections(ref cco);
        }

        internal static void SendMessage(string message)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
            var messageOptions = new SendPacketOptions()
            {
                LocalUserId = productUserId,
                RemoteUserId = productUserId,
                Data = new ArraySegment<byte>(bytes,0,bytes.Length),
                SocketId = socketId,
                Reliability = PacketReliability.ReliableOrdered,
            };
            p2pInterface.SendPacket(ref messageOptions);
        }
    }
}
