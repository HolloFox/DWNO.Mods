// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.CustomInvites
{
	/// <summary>
	/// Output parameters for the <see cref="OnCustomInviteRejectedCallback" /> Function.
	/// </summary>
	public struct CustomInviteRejectedCallbackInfo : ICallbackInfo
	{
		/// <summary>
		/// Context that was passed into <see cref="CustomInvitesInterface.AddNotifyCustomInviteRejected" />
		/// </summary>
		public object ClientData { get; set; }

		/// <summary>
		/// User that sent the custom invite
		/// </summary>
		public ProductUserId TargetUserId { get; set; }

		/// <summary>
		/// Recipient Local user id
		/// </summary>
		public ProductUserId LocalUserId { get; set; }

		/// <summary>
		/// Id of the rejected Custom Invite
		/// </summary>
		public Utf8String CustomInviteId { get; set; }

		/// <summary>
		/// Payload of the rejected Custom Invite
		/// </summary>
		public Utf8String Payload { get; set; }

		public Result? GetResultCode()
		{
			return null;
		}

		internal void Set(ref CustomInviteRejectedCallbackInfoInternal other)
		{
			ClientData = other.ClientData;
			TargetUserId = other.TargetUserId;
			LocalUserId = other.LocalUserId;
			CustomInviteId = other.CustomInviteId;
			Payload = other.Payload;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct CustomInviteRejectedCallbackInfoInternal : ICallbackInfoInternal, IGettable<CustomInviteRejectedCallbackInfo>, ISettable<CustomInviteRejectedCallbackInfo>, System.IDisposable
	{
		private System.IntPtr m_ClientData;
		private System.IntPtr m_TargetUserId;
		private System.IntPtr m_LocalUserId;
		private System.IntPtr m_CustomInviteId;
		private System.IntPtr m_Payload;

		public object ClientData
		{
			get
			{
				object value;
				Helper.Get(m_ClientData, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_ClientData);
			}
		}

		public System.IntPtr ClientDataAddress
		{
			get
			{
				return m_ClientData;
			}
		}

		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId value;
				Helper.Get(m_TargetUserId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_TargetUserId);
			}
		}

		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId value;
				Helper.Get(m_LocalUserId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_LocalUserId);
			}
		}

		public Utf8String CustomInviteId
		{
			get
			{
				Utf8String value;
				Helper.Get(m_CustomInviteId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_CustomInviteId);
			}
		}

		public Utf8String Payload
		{
			get
			{
				Utf8String value;
				Helper.Get(m_Payload, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_Payload);
			}
		}

		public void Set(ref CustomInviteRejectedCallbackInfo other)
		{
			ClientData = other.ClientData;
			TargetUserId = other.TargetUserId;
			LocalUserId = other.LocalUserId;
			CustomInviteId = other.CustomInviteId;
			Payload = other.Payload;
		}

		public void Set(ref CustomInviteRejectedCallbackInfo? other)
		{
			if (other.HasValue)
			{
				ClientData = other.Value.ClientData;
				TargetUserId = other.Value.TargetUserId;
				LocalUserId = other.Value.LocalUserId;
				CustomInviteId = other.Value.CustomInviteId;
				Payload = other.Value.Payload;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_ClientData);
			Helper.Dispose(ref m_TargetUserId);
			Helper.Dispose(ref m_LocalUserId);
			Helper.Dispose(ref m_CustomInviteId);
			Helper.Dispose(ref m_Payload);
		}

		public void Get(out CustomInviteRejectedCallbackInfo output)
		{
			output = new CustomInviteRejectedCallbackInfo();
			output.Set(ref this);
		}
	}
}