using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Services.Network.SimplePacket;
using ModLibsNet.Services.Network;


namespace ModLibsNet.Internals.NetPackets {
	/// @private
	[Serializable]
	[IsNoisy]
	class CursorPositionPacket : SimplePacketPayload {
		internal static bool BroadcastCursorIf() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				throw new ModLibsException( "Not a client." );
			}

			if( ClientCursorData.LastKnownCursorPositions.ContainsKey( Main.myPlayer ) ) {
				(int X, int Y) lastPos = ClientCursorData.LastKnownCursorPositions[ Main.myPlayer ];

				if( lastPos.X == Main.mouseX && lastPos.Y == Main.mouseY ) {
					return false;
				}
			}

			var protocol = new CursorPositionPacket( (byte)Main.myPlayer, (short)Main.mouseX, (short)Main.mouseY );
			SimplePacket.SendToClient( protocol );

			return true;
		}



		////////////////

		public byte PlayerWho;
		public short X;
		public short Y;



		////////////////

		public CursorPositionPacket() { }

		private CursorPositionPacket( byte playerWho, short x, short y ) {
			this.PlayerWho = playerWho;
			this.X = x;
			this.Y = y;
		}

		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			this.Receive();

			SimplePacket.SendToClient( this, -1, fromWho );
		}

		public override void ReceiveOnClient() {
			this.Receive();
		}


		////////////////

		private void Receive() {
			ModContent.GetInstance<ClientCursorData>()
				._LastKnownCursorPositions[this.PlayerWho] = (this.X, this.Y);
		}
	}
}