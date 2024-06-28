// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Ecom
{
	/// <summary>
	/// Input parameters for the <see cref="Transaction.CopyEntitlementByIndex" /> function.
	/// </summary>
	public struct TransactionCopyEntitlementByIndexOptions
	{
		/// <summary>
		/// The index of the entitlement to get
		/// </summary>
		public uint EntitlementIndex { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct TransactionCopyEntitlementByIndexOptionsInternal : ISettable<TransactionCopyEntitlementByIndexOptions>, System.IDisposable
	{
		private int m_ApiVersion;
		private uint m_EntitlementIndex;

		public uint EntitlementIndex
		{
			set
			{
				m_EntitlementIndex = value;
			}
		}

		public void Set(ref TransactionCopyEntitlementByIndexOptions other)
		{
			m_ApiVersion = Transaction.TransactionCopyentitlementbyindexApiLatest;
			EntitlementIndex = other.EntitlementIndex;
		}

		public void Set(ref TransactionCopyEntitlementByIndexOptions? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = Transaction.TransactionCopyentitlementbyindexApiLatest;
				EntitlementIndex = other.Value.EntitlementIndex;
			}
		}

		public void Dispose()
		{
		}
	}
}