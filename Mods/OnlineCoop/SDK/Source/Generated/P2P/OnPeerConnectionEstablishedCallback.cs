// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.P2P
{
	/// <summary>
	/// Callback for information related to new connections being established
	/// </summary>
	public delegate void OnPeerConnectionEstablishedCallback(ref OnPeerConnectionEstablishedInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(Config.LibraryCallingConvention)]
	internal delegate void OnPeerConnectionEstablishedCallbackInternal(ref OnPeerConnectionEstablishedInfoInternal data);
}