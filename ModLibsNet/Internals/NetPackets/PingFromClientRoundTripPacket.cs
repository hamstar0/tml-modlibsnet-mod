using System;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.DotNET;
using ModLibsCore.Services.Network.SimplePacket;
using ModLibsNet.Services.Net;


namespace ModLibsNet.Internals.NetPackets {
	class PingFromClientRoundTripPacket : SimplePacketPayload {
		public static void QuickSendToServer() {
			SimplePacket.SendToServer( new PingFromClientRoundTripPacket() );
		}



		////////////////

		public long StartTime = -1;
		public long ServerBounceTime = -1;
		public long RoundTripTime = -1;



		////////////////

		public PingFromClientRoundTripPacket() {
			this.StartTime = (long)SystemLibraries.TimeStamp().TotalMilliseconds;
		}


		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			this.ServerBounceTime = (long)SystemLibraries.TimeStamp().TotalMilliseconds;

			SimplePacket.SendToClient( this, fromWho, -1 );
		}


		public override void ReceiveOnClient() {
			if( this.ServerBounceTime == -1 ) {
				throw new ModLibsException( "Improper ping gauging." );
			}

			this.RoundTripTime = (long)SystemLibraries.TimeStamp().TotalMilliseconds;

			if( this.ServerBounceTime == -1 ) {
			//	SimplePacket.SendToServer( this );
			} else {
				int upSpan = (int)( this.ServerBounceTime - this.StartTime );
				int downSpan = (int)( this.RoundTripTime - this.ServerBounceTime );
				int allSpan = (int)( this.RoundTripTime - this.StartTime );

				ModContent.GetInstance<Ping>().UpdatePing( upSpan, downSpan, allSpan );
			}
		}
	}
}