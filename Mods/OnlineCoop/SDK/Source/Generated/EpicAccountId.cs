// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices
{
	public sealed partial class EpicAccountId : Handle
	{
		public EpicAccountId()
		{
		}

		public EpicAccountId(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// A character buffer of this size is large enough to fit a successful output of <see cref="ToString" />. This length does not include the null-terminator.
		/// The EpicAccountId data structure is opaque in nature and no assumptions of its structure should be inferred
		/// </summary>
		public const int EpicaccountidMaxLength = 32;

		/// <summary>
		/// Retrieve an <see cref="EpicAccountId" /> from a raw string representing an Epic Account ID. The input string must be null-terminated.
		/// NOTE: There is no validation on the string format, this should only be used with values serialized from legitimate sources such as <see cref="ToString" />
		/// </summary>
		/// <param name="accountIdString">The stringified account ID for which to retrieve the Epic Account ID</param>
		/// <returns>
		/// The <see cref="EpicAccountId" /> that corresponds to the AccountIdString
		/// </returns>
		public static EpicAccountId FromString(Utf8String accountIdString)
		{
			var accountIdStringAddress = System.IntPtr.Zero;
			Helper.Set(accountIdString, ref accountIdStringAddress);

			var funcResult = Bindings.EOS_EpicAccountId_FromString(accountIdStringAddress);

			Helper.Dispose(ref accountIdStringAddress);

			EpicAccountId funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		public static explicit operator EpicAccountId(Utf8String value)
		{
			return FromString(value);
		}

		/// <summary>
		/// Check whether or not the given Epic Account ID is considered valid
		/// NOTE: This will return true for any <see cref="EpicAccountId" /> created with <see cref="FromString" /> as there is no validation
		/// </summary>
		/// <param name="accountId">The Epic Account ID to check for validity</param>
		/// <returns>
		/// <see langword="true" /> if the <see cref="EpicAccountId" /> is valid, otherwise <see langword="false" />
		/// </returns>
		public bool IsValid()
		{
			var funcResult = Bindings.EOS_EpicAccountId_IsValid(InnerHandle);

			bool funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Retrieve a null-terminated stringified Epic Account ID from an <see cref="EpicAccountId" />. This is useful for replication of Epic Account IDs in multiplayer games.
		/// This string will be no larger than <see cref="EpicaccountidMaxLength" /> + 1 and will only contain UTF8-encoded printable characters as well as a null-terminator.
		/// </summary>
		/// <param name="accountId">The Epic Account ID for which to retrieve the stringified version.</param>
		/// <param name="outBuffer">The buffer into which the character data should be written</param>
		/// <param name="inOutBufferLength">
		/// The size of the OutBuffer in characters.
		/// The input buffer should include enough space to be null-terminated.
		/// When the function returns, this parameter will be filled with the length of the string copied into OutBuffer including the null-termination character.
		/// </param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the Epic Account ID string was copied into the OutBuffer.
		/// <see cref="Result.Success" /> - The OutBuffer was filled, and InOutBufferLength contains the number of characters copied into OutBuffer including the null-terminator.
		/// <see cref="Result.InvalidParameters" /> - Either OutBuffer or InOutBufferLength were passed as <see langword="null" /> parameters.
		/// <see cref="Result.InvalidUser" /> - The AccountId is invalid and cannot be stringified.
		/// <see cref="Result.LimitExceeded" /> - The OutBuffer is not large enough to receive the Epic Account ID string. InOutBufferLength contains the required minimum length to perform the operation successfully.
		/// </returns>
		public Result ToString(out Utf8String outBuffer)
		{
			int inOutBufferLength = EpicaccountidMaxLength + 1;
			System.IntPtr outBufferAddress = Helper.AddAllocation(inOutBufferLength);

			var funcResult = Bindings.EOS_EpicAccountId_ToString(InnerHandle, outBufferAddress, ref inOutBufferLength);

			Helper.Get(outBufferAddress, out outBuffer);
			Helper.Dispose(ref outBufferAddress);

			return funcResult;
		}

		public override string ToString()
		{
			Utf8String funcResult;
			ToString(out funcResult);
			return funcResult;
		}

		public override string ToString(string format, System.IFormatProvider formatProvider)
		{
			if (format != null)
			{
				return string.Format(format, ToString());
			}

			return ToString();
		}

		public static explicit operator Utf8String(EpicAccountId value)
		{
			Utf8String result = null;

			if (value != null)
			{
				value.ToString(out result);
			}

			return result;
		}
	}
}