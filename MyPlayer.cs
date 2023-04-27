using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsNet.Internals.NetPackets;


namespace ModLibsNet.Internals.Logic {
	/// @private
	partial class ModLibsNetPlayer : ModPlayer {
		private int TestPing = 0;



		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) { // Current player
				this.PreUpdateLocal();
			}
		}


		private void PreUpdateLocal() {
			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				this.PreUpdateCurrentClient();
			}
		}

		private void PreUpdateCurrentClient() {
			// Update ping every 15 seconds
			if( ModLibsNetConfig.Instance.IsClientsGaugingAveragePing ) {
				if( this.TestPing++ > ModLibsNetConfig.Instance.PingUpdateDelay ) {
					this.TestPing = 0;

					PingFromClientRoundTripPacket.QuickSendToServer();
				}
			}
		}
	}
}
