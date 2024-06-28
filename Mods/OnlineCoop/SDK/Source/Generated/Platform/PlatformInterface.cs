// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

using System.Reflection;

namespace Epic.OnlineServices.Platform
{
	public sealed partial class PlatformInterface : Handle
	{
		public PlatformInterface()
		{
		}

		public PlatformInterface(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// The name of the env var used to determine if the game was launched by the Epic Games Launcher.
		/// 
		/// During the call to <see cref="Create" />, the command line that was used to launch the app is inspected, and if it is
		/// recognized as coming from the Epic Games Launcher, this environment variable is set to 1.
		/// 
		/// NOTE: You can force the <see cref="CheckForLauncherAndRestart" /> API to relaunch the title by
		/// explicitly unsetting this environment variable before calling <see cref="CheckForLauncherAndRestart" />.
		/// </summary>
		public static readonly Utf8String CheckforlauncherandrestartEnvVar = "EOS_LAUNCHED_BY_EPIC";

		/// <summary>
		/// Max length of a client id, not including the terminating null.
		/// </summary>
		public const int ClientcredentialsClientidMaxLength = 64;

		/// <summary>
		/// Max length of a client secret, not including the terminating null.
		/// </summary>
		public const int ClientcredentialsClientsecretMaxLength = 64;

		public const int CountrycodeMaxBufferLen = (CountrycodeMaxLength + 1);

		public const int CountrycodeMaxLength = 4;

		/// <summary>
		/// The most recent version of the <see cref="GetDesktopCrossplayStatus" /> API.
		/// </summary>
		public const int GetdesktopcrossplaystatusApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="Initialize" /> API.
		/// </summary>
		public const int InitializeApiLatest = 4;

		/// <summary>
		/// The most recent version of the <see cref="InitializeThreadAffinity" /> API.
		/// </summary>
		public const int InitializeThreadaffinityApiLatest = 2;

		/// <summary>
		/// Max length of a product name, not including the terminating null.
		/// </summary>
		public const int InitializeoptionsProductnameMaxLength = 64;

		/// <summary>
		/// Max length of a product version, not including the terminating null.
		/// </summary>
		public const int InitializeoptionsProductversionMaxLength = 64;

		public const int LocalecodeMaxBufferLen = (LocalecodeMaxLength + 1);

		public const int LocalecodeMaxLength = 9;

		public const int OptionsApiLatest = 13;

		/// <summary>
		/// Max length of a deployment id, not including the terminating null.
		/// </summary>
		public const int OptionsDeploymentidMaxLength = 64;

		/// <summary>
		/// Length of an encryption key, not including the terminating null.
		/// </summary>
		public const int OptionsEncryptionkeyLength = 64;

		/// <summary>
		/// Max length of a product id, not including the terminating null.
		/// </summary>
		public const int OptionsProductidMaxLength = 64;

		/// <summary>
		/// Max length of a sandbox id, not including the terminating null.
		/// </summary>
		public const int OptionsSandboxidMaxLength = 64;

		/// <summary>
		/// The most recent version of the <see cref="RTCOptions" /> API.
		/// </summary>
		public const int RtcoptionsApiLatest = 2;

		/// <summary>
		/// Checks if the app was launched through the Epic Games Launcher, and relaunches it through the Epic Games Launcher if it wasn't.
		/// 
		/// NOTE: During the call to <see cref="Create" />, the command line that was used to launch the app is inspected, and if it is
		/// recognized as coming from the Epic Games Launcher, an environment variable is set to 1. The name of the environment variable
		/// is defined by <see cref="CheckforlauncherandrestartEnvVar" />.
		/// 
		/// You can force the <see cref="CheckForLauncherAndRestart" /> API to relaunch the title by
		/// explicitly unsetting this environment variable before calling <see cref="CheckForLauncherAndRestart" />.
		/// </summary>
		/// <returns>
		/// An <see cref="Result" /> is returned to indicate success or an error.
		/// <see cref="Result.Success" /> is returned if the app is being restarted. You should quit your process as soon as possible.
		/// <see cref="Result.NoChange" /> is returned if the app was already launched through the Epic Launcher, and no action needs to be taken.
		/// <see cref="Result.UnexpectedError" /> is returned if the LauncherCheck module failed to initialize, or the module tried and failed to restart the app.
		/// </returns>
		public Result CheckForLauncherAndRestart()
		{
			var funcResult = Bindings.EOS_Platform_CheckForLauncherAndRestart(InnerHandle);

			return funcResult;
		}

		/// <summary>
		/// Create a single Epic Online Services Platform Instance.
		/// 
		/// The platform instance is used to gain access to the various Epic Online Services.
		/// 
		/// This function returns an opaque handle to the platform instance, and that handle must be passed to <see cref="Release" /> to release the instance.
		/// </summary>
		/// <returns>
		/// An opaque handle to the platform instance.
		/// </returns>
		public static PlatformInterface Create(ref Options options)
		{
			OptionsInternal optionsInternal = new OptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_Platform_Create(ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			PlatformInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Achievements Interface.
		/// eos_achievements.h
		/// eos_achievements_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Achievements.AchievementsInterface" /> handle
		/// </returns>
		public Achievements.AchievementsInterface GetAchievementsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetAchievementsInterface(InnerHandle);

			Achievements.AchievementsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// This only will return the value set as the override otherwise <see cref="Result.NotFound" /> is returned.
		/// This is not currently used for anything internally.
		/// eos_ecom.h
		/// <seealso cref="CountrycodeMaxLength" />
		/// </summary>
		/// <param name="localUserId">The account to use for lookup if no override exists.</param>
		/// <param name="outBuffer">The buffer into which the character data should be written. The buffer must be long enough to hold a string of <see cref="CountrycodeMaxLength" />.</param>
		/// <param name="inOutBufferLength">
		/// The size of the OutBuffer in characters.
		/// The input buffer should include enough space to be null-terminated.
		/// When the function returns, this parameter will be filled with the length of the string copied into OutBuffer.
		/// </param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the active country code string was copied into the OutBuffer.
		/// <see cref="Result.Success" /> if the information is available and passed out in OutBuffer
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if there is not an override country code for the user.
		/// <see cref="Result.LimitExceeded" /> - The OutBuffer is not large enough to receive the country code string. InOutBufferLength contains the required minimum length to perform the operation successfully.
		/// </returns>
		public Result GetActiveCountryCode(EpicAccountId localUserId, out Utf8String outBuffer)
		{
			var localUserIdInnerHandle = System.IntPtr.Zero;
			Helper.Set(localUserId, ref localUserIdInnerHandle);

			int inOutBufferLength = CountrycodeMaxLength + 1;
			System.IntPtr outBufferAddress = Helper.AddAllocation(inOutBufferLength);

			var funcResult = Bindings.EOS_Platform_GetActiveCountryCode(InnerHandle, localUserIdInnerHandle, outBufferAddress, ref inOutBufferLength);

			Helper.Get(outBufferAddress, out outBuffer);
			Helper.Dispose(ref outBufferAddress);

			return funcResult;
		}

		/// <summary>
		/// Get the active locale code that the SDK will send to services which require it.
		/// This returns the override value otherwise it will use the locale code of the given user.
		/// This is used for localization. This follows ISO 639.
		/// eos_ecom.h
		/// <seealso cref="LocalecodeMaxLength" />
		/// </summary>
		/// <param name="localUserId">The account to use for lookup if no override exists.</param>
		/// <param name="outBuffer">The buffer into which the character data should be written. The buffer must be long enough to hold a string of <see cref="LocalecodeMaxLength" />.</param>
		/// <param name="inOutBufferLength">
		/// The size of the OutBuffer in characters.
		/// The input buffer should include enough space to be null-terminated.
		/// When the function returns, this parameter will be filled with the length of the string copied into OutBuffer.
		/// </param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the active locale code string was copied into the OutBuffer.
		/// <see cref="Result.Success" /> if the information is available and passed out in OutBuffer
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if there is neither an override nor an available locale code for the user.
		/// <see cref="Result.LimitExceeded" /> - The OutBuffer is not large enough to receive the locale code string. InOutBufferLength contains the required minimum length to perform the operation successfully.
		/// </returns>
		public Result GetActiveLocaleCode(EpicAccountId localUserId, out Utf8String outBuffer)
		{
			var localUserIdInnerHandle = System.IntPtr.Zero;
			Helper.Set(localUserId, ref localUserIdInnerHandle);

			int inOutBufferLength = LocalecodeMaxLength + 1;
			System.IntPtr outBufferAddress = Helper.AddAllocation(inOutBufferLength);

			var funcResult = Bindings.EOS_Platform_GetActiveLocaleCode(InnerHandle, localUserIdInnerHandle, outBufferAddress, ref inOutBufferLength);

			Helper.Get(outBufferAddress, out outBuffer);
			Helper.Dispose(ref outBufferAddress);

			return funcResult;
		}

		/// <summary>
		/// Get a handle to the Anti-Cheat Client Interface.
		/// eos_anticheatclient.h
		/// eos_anticheatclient_types.h
		/// </summary>
		/// <returns>
		/// <see cref="AntiCheatClient.AntiCheatClientInterface" /> handle
		/// </returns>
		public AntiCheatClient.AntiCheatClientInterface GetAntiCheatClientInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetAntiCheatClientInterface(InnerHandle);

			AntiCheatClient.AntiCheatClientInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Anti-Cheat Server Interface.
		/// eos_anticheatserver.h
		/// eos_anticheatserver_types.h
		/// </summary>
		/// <returns>
		/// <see cref="AntiCheatServer.AntiCheatServerInterface" /> handle
		/// </returns>
		public AntiCheatServer.AntiCheatServerInterface GetAntiCheatServerInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetAntiCheatServerInterface(InnerHandle);

			AntiCheatServer.AntiCheatServerInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Retrieves the current application state as told to the SDK by the application.
		/// </summary>
		/// <returns>
		/// The current application status.
		/// </returns>
		public ApplicationStatus GetApplicationStatus()
		{
			var funcResult = Bindings.EOS_Platform_GetApplicationStatus(InnerHandle);

			return funcResult;
		}

		/// <summary>
		/// Get a handle to the Auth Interface.
		/// eos_auth.h
		/// eos_auth_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Auth.AuthInterface" /> handle
		/// </returns>
		public Auth.AuthInterface GetAuthInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetAuthInterface(InnerHandle);

			Auth.AuthInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Connect Interface.
		/// eos_connect.h
		/// eos_connect_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Connect.ConnectInterface" /> handle
		/// </returns>
		public Connect.ConnectInterface GetConnectInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetConnectInterface(InnerHandle);

			Connect.ConnectInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Custom Invites Interface.
		/// eos_custominvites.h
		/// eos_custominvites_types.h
		/// </summary>
		/// <returns>
		/// <see cref="CustomInvites.CustomInvitesInterface" /> handle
		/// </returns>
		public CustomInvites.CustomInvitesInterface GetCustomInvitesInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetCustomInvitesInterface(InnerHandle);

			CustomInvites.CustomInvitesInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Windows only.
		/// Checks that the application is ready to use desktop crossplay functionality, with the necessary prerequisites having been met.
		/// 
		/// This function verifies that the application was launched through the Bootstrapper application,
		/// the redistributable service has been installed and is running in the background,
		/// and that the overlay has been loaded successfully.
		/// 
		/// On Windows, the desktop crossplay functionality is required to use Epic accounts login
		/// with applications that are distributed outside the Epic Games Store.
		/// </summary>
		/// <param name="options">input structure that specifies the API version.</param>
		/// <param name="outDesktopCrossplayStatusInfo">output structure to receive the desktop crossplay status information.</param>
		/// <returns>
		/// An <see cref="Result" /> is returned to indicate success or an error.
		/// <see cref="Result.NotImplemented" /> is returned on non-Windows platforms.
		/// </returns>
		public Result GetDesktopCrossplayStatus(ref GetDesktopCrossplayStatusOptions options, out DesktopCrossplayStatusInfo outDesktopCrossplayStatusInfo)
		{
			GetDesktopCrossplayStatusOptionsInternal optionsInternal = new GetDesktopCrossplayStatusOptionsInternal();
			optionsInternal.Set(ref options);

			var outDesktopCrossplayStatusInfoInternal = Helper.GetDefault<DesktopCrossplayStatusInfoInternal>();

			var funcResult = Bindings.EOS_Platform_GetDesktopCrossplayStatus(InnerHandle, ref optionsInternal, ref outDesktopCrossplayStatusInfoInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.Get(ref outDesktopCrossplayStatusInfoInternal, out outDesktopCrossplayStatusInfo);

			return funcResult;
		}

		/// <summary>
		/// Get a handle to the Ecom Interface.
		/// eos_ecom.h
		/// eos_ecom_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Ecom.EcomInterface" /> handle
		/// </returns>
		public Ecom.EcomInterface GetEcomInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetEcomInterface(InnerHandle);

			Ecom.EcomInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Friends Interface.
		/// eos_friends.h
		/// eos_friends_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Friends.FriendsInterface" /> handle
		/// </returns>
		public Friends.FriendsInterface GetFriendsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetFriendsInterface(InnerHandle);

			Friends.FriendsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Integrated Platform Interface.
		/// eos_integratedplatform.h
		/// eos_integratedplatform_types.h
		/// </summary>
		/// <returns>
		/// <see cref="IntegratedPlatform.IntegratedPlatformInterface" /> handle
		/// </returns>
		public IntegratedPlatform.IntegratedPlatformInterface GetIntegratedPlatformInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetIntegratedPlatformInterface(InnerHandle);

			IntegratedPlatform.IntegratedPlatformInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Kids Web Service Interface.
		/// eos_kws.h
		/// eos_kws_types.h
		/// </summary>
		/// <returns>
		/// <see cref="KWS.KWSInterface" /> handle
		/// </returns>
		public KWS.KWSInterface GetKWSInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetKWSInterface(InnerHandle);

			KWS.KWSInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Leaderboards Interface.
		/// eos_leaderboards.h
		/// eos_leaderboards_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Leaderboards.LeaderboardsInterface" /> handle
		/// </returns>
		public Leaderboards.LeaderboardsInterface GetLeaderboardsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetLeaderboardsInterface(InnerHandle);

			Leaderboards.LeaderboardsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Lobby Interface.
		/// eos_lobby.h
		/// eos_lobby_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Lobby.LobbyInterface" /> handle
		/// </returns>
		public Lobby.LobbyInterface GetLobbyInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetLobbyInterface(InnerHandle);

			Lobby.LobbyInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Metrics Interface.
		/// eos_metrics.h
		/// eos_metrics_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Metrics.MetricsInterface" /> handle
		/// </returns>
		public Metrics.MetricsInterface GetMetricsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetMetricsInterface(InnerHandle);

			Metrics.MetricsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Mods Interface.
		/// eos_mods.h
		/// eos_mods_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Mods.ModsInterface" /> handle
		/// </returns>
		public Mods.ModsInterface GetModsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetModsInterface(InnerHandle);

			Mods.ModsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Retrieves the current network state as told to the SDK by the application.
		/// </summary>
		/// <returns>
		/// The current network status.
		/// </returns>
		public NetworkStatus GetNetworkStatus()
		{
			var funcResult = Bindings.EOS_Platform_GetNetworkStatus(InnerHandle);

			return funcResult;
		}

		/// <summary>
		/// Get the override country code that the SDK will send to services which require it.
		/// This is not currently used for anything internally.
		/// eos_ecom.h
		/// <seealso cref="CountrycodeMaxLength" />
		/// </summary>
		/// <param name="outBuffer">The buffer into which the character data should be written. The buffer must be long enough to hold a string of <see cref="CountrycodeMaxLength" />.</param>
		/// <param name="inOutBufferLength">
		/// The size of the OutBuffer in characters.
		/// The input buffer should include enough space to be null-terminated.
		/// When the function returns, this parameter will be filled with the length of the string copied into OutBuffer.
		/// </param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the override country code string was copied into the OutBuffer.
		/// <see cref="Result.Success" /> if the information is available and passed out in OutBuffer
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.LimitExceeded" /> - The OutBuffer is not large enough to receive the country code string. InOutBufferLength contains the required minimum length to perform the operation successfully.
		/// </returns>
		public Result GetOverrideCountryCode(out Utf8String outBuffer)
		{
			int inOutBufferLength = CountrycodeMaxLength + 1;
			System.IntPtr outBufferAddress = Helper.AddAllocation(inOutBufferLength);

			var funcResult = Bindings.EOS_Platform_GetOverrideCountryCode(InnerHandle, outBufferAddress, ref inOutBufferLength);

			Helper.Get(outBufferAddress, out outBuffer);
			Helper.Dispose(ref outBufferAddress);

			return funcResult;
		}

		/// <summary>
		/// Get the override locale code that the SDK will send to services which require it.
		/// This is used for localization. This follows ISO 639.
		/// eos_ecom.h
		/// <seealso cref="LocalecodeMaxLength" />
		/// </summary>
		/// <param name="outBuffer">The buffer into which the character data should be written. The buffer must be long enough to hold a string of <see cref="LocalecodeMaxLength" />.</param>
		/// <param name="inOutBufferLength">
		/// The size of the OutBuffer in characters.
		/// The input buffer should include enough space to be null-terminated.
		/// When the function returns, this parameter will be filled with the length of the string copied into OutBuffer.
		/// </param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the override locale code string was copied into the OutBuffer.
		/// <see cref="Result.Success" /> if the information is available and passed out in OutBuffer
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.LimitExceeded" /> - The OutBuffer is not large enough to receive the locale code string. InOutBufferLength contains the required minimum length to perform the operation successfully.
		/// </returns>
		public Result GetOverrideLocaleCode(out Utf8String outBuffer)
		{
			int inOutBufferLength = LocalecodeMaxLength + 1;
			System.IntPtr outBufferAddress = Helper.AddAllocation(inOutBufferLength);

			var funcResult = Bindings.EOS_Platform_GetOverrideLocaleCode(InnerHandle, outBufferAddress, ref inOutBufferLength);

			Helper.Get(outBufferAddress, out outBuffer);
			Helper.Dispose(ref outBufferAddress);

			return funcResult;
		}

		/// <summary>
		/// Get a handle to the Peer-to-Peer Networking Interface.
		/// eos_p2p.h
		/// eos_p2p_types.h
		/// </summary>
		/// <returns>
		/// <see cref="P2P.P2PInterface" /> handle
		/// </returns>
		public P2P.P2PInterface GetP2PInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetP2PInterface(InnerHandle);

			P2P.P2PInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the PlayerDataStorage Interface.
		/// eos_playerdatastorage.h
		/// eos_playerdatastorage_types.h
		/// </summary>
		/// <returns>
		/// <see cref="PlayerDataStorage.PlayerDataStorageInterface" /> handle
		/// </returns>
		public PlayerDataStorage.PlayerDataStorageInterface GetPlayerDataStorageInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetPlayerDataStorageInterface(InnerHandle);

			PlayerDataStorage.PlayerDataStorageInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Presence Interface.
		/// eos_presence.h
		/// eos_presence_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Presence.PresenceInterface" /> handle
		/// </returns>
		public Presence.PresenceInterface GetPresenceInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetPresenceInterface(InnerHandle);

			Presence.PresenceInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get the active country code that the SDK will send to services which require it.
		/// This returns the override value otherwise it will use the country code of the given user.
		/// This is currently used for determining pricing.
		/// Get a handle to the ProgressionSnapshot Interface.
		/// eos_progressionsnapshot.h
		/// eos_progressionsnapshot_types.h
		/// </summary>
		/// <returns>
		/// <see cref="ProgressionSnapshot.ProgressionSnapshotInterface" /> handle
		/// </returns>
		public ProgressionSnapshot.ProgressionSnapshotInterface GetProgressionSnapshotInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetProgressionSnapshotInterface(InnerHandle);

			ProgressionSnapshot.ProgressionSnapshotInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the RTC Admin interface
		/// eos_rtc_admin.h
		/// eos_admin_types.h
		/// </summary>
		/// <returns>
		/// <see cref="RTCAdmin.RTCAdminInterface" /> handle
		/// </returns>
		public RTCAdmin.RTCAdminInterface GetRTCAdminInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetRTCAdminInterface(InnerHandle);

			RTCAdmin.RTCAdminInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Real Time Communications Interface (RTC).
		/// From the RTC interface you can retrieve the handle to the audio interface (RTCAudio), which is a component of RTC.
		/// <seealso cref="RTC.RTCInterface.GetAudioInterface" />
		/// eos_rtc.h
		/// eos_rtc_types.h
		/// </summary>
		/// <returns>
		/// <see cref="RTC.RTCInterface" /> handle
		/// </returns>
		public RTC.RTCInterface GetRTCInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetRTCInterface(InnerHandle);

			RTC.RTCInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Reports Interface.
		/// eos_reports.h
		/// eos_reports_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Reports.ReportsInterface" /> handle
		/// </returns>
		public Reports.ReportsInterface GetReportsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetReportsInterface(InnerHandle);

			Reports.ReportsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Sanctions Interface.
		/// eos_sanctions.h
		/// eos_sanctions_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Sanctions.SanctionsInterface" /> handle
		/// </returns>
		public Sanctions.SanctionsInterface GetSanctionsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetSanctionsInterface(InnerHandle);

			Sanctions.SanctionsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Sessions Interface.
		/// eos_sessions.h
		/// eos_sessions_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Sessions.SessionsInterface" /> handle
		/// </returns>
		public Sessions.SessionsInterface GetSessionsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetSessionsInterface(InnerHandle);

			Sessions.SessionsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the Stats Interface.
		/// eos_stats.h
		/// eos_stats_types.h
		/// </summary>
		/// <returns>
		/// <see cref="Stats.StatsInterface" /> handle
		/// </returns>
		public Stats.StatsInterface GetStatsInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetStatsInterface(InnerHandle);

			Stats.StatsInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the TitleStorage Interface.
		/// eos_titlestorage.h
		/// eos_titlestorage_types.h
		/// </summary>
		/// <returns>
		/// <see cref="TitleStorage.TitleStorageInterface" /> handle
		/// </returns>
		public TitleStorage.TitleStorageInterface GetTitleStorageInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetTitleStorageInterface(InnerHandle);

			TitleStorage.TitleStorageInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the UI Interface.
		/// eos_ui.h
		/// eos_ui_types.h
		/// </summary>
		/// <returns>
		/// <see cref="UI.UIInterface" /> handle
		/// </returns>
		public UI.UIInterface GetUIInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetUIInterface(InnerHandle);

			UI.UIInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Get a handle to the UserInfo Interface.
		/// eos_userinfo.h
		/// eos_userinfo_types.h
		/// </summary>
		/// <returns>
		/// <see cref="UserInfo.UserInfoInterface" /> handle
		/// </returns>
		public UserInfo.UserInfoInterface GetUserInfoInterface()
		{
			var funcResult = Bindings.EOS_Platform_GetUserInfoInterface(InnerHandle);

			UserInfo.UserInfoInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Initialize the Epic Online Services SDK.
		/// 
		/// Before calling any other function in the SDK, clients must call this function.
		/// 
		/// This function must only be called one time and must have a corresponding <see cref="Shutdown" /> call.
		/// </summary>
		/// <param name="options">- The initialization options to use for the SDK.</param>
		/// <returns>
		/// An <see cref="Result" /> is returned to indicate success or an error.
		/// <see cref="Result.Success" /> is returned if the SDK successfully initializes.
		/// <see cref="Result.AlreadyConfigured" /> is returned if the function has already been called.
		/// <see cref="Result.InvalidParameters" /> is returned if the provided options are invalid.
		/// </returns>
		public static Result Initialize(ref InitializeOptions options)
		{
            EOSPlugin.EOSPlugin.logSource.LogDebug("Construct InitializeOptionsInternal");
            InitializeOptionsInternal optionsInternal = new InitializeOptionsInternal();

            EOSPlugin.EOSPlugin.logSource.LogDebug("Construct Set Options");
            optionsInternal.Set(ref options);

            //EOSPlugin.EOSPlugin.logSource.LogDebug("Bindings EOS Initialize");
            var funcResult = Bindings.EOS_Initialize(ref optionsInternal);

            EOSPlugin.EOSPlugin.logSource.LogDebug("Dispose Helper");
            Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Release an Epic Online Services platform instance previously returned from <see cref="Create" />.
		/// 
		/// This function should only be called once per instance returned by <see cref="Create" />. Undefined behavior will result in calling it with a single instance more than once.
		/// Typically only a single platform instance needs to be created during the lifetime of a game.
		/// You should release each platform instance before calling the <see cref="Shutdown" /> function.
		/// </summary>
		public void Release()
		{
			Bindings.EOS_Platform_Release(InnerHandle);
		}

		/// <summary>
		/// Notify a change in application state.
		/// Calling SetApplicationStatus must happen before Tick when foregrounding for the cases where we won't get the background notification.
		/// </summary>
		/// <param name="newStatus">The new status for the application.</param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether we changed the application status successfully.
		/// <see cref="Result.Success" /> if the application was changed successfully.
		/// <see cref="Result.InvalidParameters" /> if the value of NewStatus is invalid.
		/// <see cref="Result.NotImplemented" /> if <see cref="ApplicationStatus.BackgroundConstrained" /> or <see cref="ApplicationStatus.BackgroundUnconstrained" /> are attempted to be set on platforms that do not have such application states.
		/// </returns>
		public Result SetApplicationStatus(ApplicationStatus newStatus)
		{
			var funcResult = Bindings.EOS_Platform_SetApplicationStatus(InnerHandle, newStatus);

			return funcResult;
		}

		/// <summary>
		/// Notify a change in network state.
		/// </summary>
		/// <param name="newStatus">The new network status.</param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether we changed the network status successfully.
		/// <see cref="Result.Success" /> if the network was changed successfully.
		/// <see cref="Result.InvalidParameters" /> if the value of NewStatus is invalid.
		/// </returns>
		public Result SetNetworkStatus(NetworkStatus newStatus)
		{
			var funcResult = Bindings.EOS_Platform_SetNetworkStatus(InnerHandle, newStatus);

			return funcResult;
		}

		/// <summary>
		/// Set the override country code that the SDK will send to services which require it.
		/// This is not currently used for anything internally.
		/// eos_ecom.h
		/// <seealso cref="CountrycodeMaxLength" />
		/// </summary>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the override country code string was saved.
		/// <see cref="Result.Success" /> if the country code was overridden
		/// <see cref="Result.InvalidParameters" /> if you pass an invalid country code
		/// </returns>
		public Result SetOverrideCountryCode(Utf8String newCountryCode)
		{
			var newCountryCodeAddress = System.IntPtr.Zero;
			Helper.Set(newCountryCode, ref newCountryCodeAddress);

			var funcResult = Bindings.EOS_Platform_SetOverrideCountryCode(InnerHandle, newCountryCodeAddress);

			Helper.Dispose(ref newCountryCodeAddress);

			return funcResult;
		}

		/// <summary>
		/// Set the override locale code that the SDK will send to services which require it.
		/// This is used for localization. This follows ISO 639.
		/// eos_ecom.h
		/// <seealso cref="LocalecodeMaxLength" />
		/// </summary>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the override locale code string was saved.
		/// <see cref="Result.Success" /> if the locale code was overridden
		/// <see cref="Result.InvalidParameters" /> if you pass an invalid locale code
		/// </returns>
		public Result SetOverrideLocaleCode(Utf8String newLocaleCode)
		{
			var newLocaleCodeAddress = System.IntPtr.Zero;
			Helper.Set(newLocaleCode, ref newLocaleCodeAddress);

			var funcResult = Bindings.EOS_Platform_SetOverrideLocaleCode(InnerHandle, newLocaleCodeAddress);

			Helper.Dispose(ref newLocaleCodeAddress);

			return funcResult;
		}

		/// <summary>
		/// Tear down the Epic Online Services SDK.
		/// 
		/// Once this function has been called, no more SDK calls are permitted; calling anything after <see cref="Shutdown" /> will result in undefined behavior.
		/// </summary>
		/// <returns>
		/// An <see cref="Result" /> is returned to indicate success or an error.
		/// <see cref="Result.Success" /> is returned if the SDK is successfully torn down.
		/// <see cref="Result.NotConfigured" /> is returned if a successful call to <see cref="Initialize" /> has not been made.
		/// <see cref="Result.UnexpectedError" /> is returned if <see cref="Shutdown" /> has already been called.
		/// </returns>
		public static Result Shutdown()
		{
			var funcResult = Bindings.EOS_Shutdown();

			return funcResult;
		}

		/// <summary>
		/// Notify the platform instance to do work. This function must be called frequently in order for the services provided by the SDK to properly
		/// function. For tick-based applications, it is usually desirable to call this once per-tick.
		/// </summary>
		public void Tick()
		{
			Bindings.EOS_Platform_Tick(InnerHandle);
		}

		/// <summary>
		/// Gets the string representation of an <see cref="ApplicationStatus" /> value.
		/// 
		/// Example: <see cref="ToString" />(<see cref="ApplicationStatus.Foreground" />) returns "EOS_AS_Foreground".
		/// </summary>
		/// <param name="applicationStatus"><see cref="ApplicationStatus" /> value to get as string.</param>
		/// <returns>
		/// Pointer to a static string representing the input enum value.
		/// The returned string is guaranteed to be non-null, and must not be freed by the application.
		/// </returns>
		public static Utf8String ToString(ApplicationStatus applicationStatus)
		{
			var funcResult = Bindings.EOS_EApplicationStatus_ToString(applicationStatus);

			Utf8String funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Gets the string representation of an <see cref="NetworkStatus" /> value.
		/// 
		/// Example: <see cref="ToString" />(<see cref="NetworkStatus.Online" />) returns "EOS_NS_Online".
		/// </summary>
		/// <param name="networkStatus"><see cref="NetworkStatus" /> value to get as string.</param>
		/// <returns>
		/// Pointer to a static string representing the input enum value.
		/// The returned string is guaranteed to be non-null, and must not be freed by the application.
		/// </returns>
		public static Utf8String ToString(NetworkStatus networkStatus)
		{
			var funcResult = Bindings.EOS_ENetworkStatus_ToString(networkStatus);

			Utf8String funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}
	}
}