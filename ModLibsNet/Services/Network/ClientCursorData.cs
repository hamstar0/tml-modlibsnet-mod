using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;
using ModLibsCore.Services.Timers;
using ModLibsNet.Internals.NetPackets;


namespace ModLibsNet.Services.Network {
	/// <summary>
	/// Supplies assorted server informations and tools.
	/// </summary>
	public partial class ClientCursorData : ILoadable {
		/// <summary>
		/// Shows last known positions of each player's mouse cursor. Must be activated via. StartBroadcastingMyCursorPosition(),
		/// first.
		/// </summary>
		public static IReadOnlyDictionary<int, (short X, short Y)> LastKnownCursorPositions { get; private set; }



		////////////////

		/// <summary>
		/// Begins a broadcast loop (via. Timers) every 1/4 second to tell everyone where the current player's cursor is located.
		/// </summary>
		/// <returns>`true` if loop has begun (and wasn't already).</returns>
		public static bool StartBroadcastingMyCursorPosition() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				throw new ModLibsException( "Not a client." );
			}

//LogLibraries.LogOnce( "UUU StartBroadcastingMyCursorPosition - "+string.Join("\n  ", DebugLibraries.GetContextSlice()) );
			string timerName = "cursor_broadcast_" + Main.myPlayer;
			if( Timers.GetTimerTickDuration(timerName) > 0 ) {
				return false;
			}

			Timers.SetTimer( timerName, 15, false, () => {
				bool canBroadcast = Main.player[Main.myPlayer].active
					&& LoadLibraries.IsWorldBeingPlayed();

				if( canBroadcast ) {
					CursorPositionPacket.BroadcastCursorIf();
				}

				return canBroadcast;
			} );

			return true;
		}


		/// <summary>
		/// Ends the current cursor position broadcast loop.
		/// </summary>
		public static void StopBroadcastingMyCursorPosition() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				throw new ModLibsException( "Not a client." );
			}

			string timerName = "cursor_broadcast_" + Main.myPlayer;

			Timers.UnsetTimer( timerName );
		}
	}
}
