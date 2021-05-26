using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace ModLibsNet.Services.Net {
	/// <summary>
	/// Assorted static "helper" functions pertaining to network play.
	/// </summary>
	public partial class Ping {
		/// <summary>
		/// Gets the most recent gauging of ping between current client and server (and back).
		/// </summary>
		/// <returns></returns>
		public static int GetServerPing() {
			if( Main.netMode != NetmodeID.MultiplayerClient ) {
				throw new ModLibsException( "Only clients can gauge ping." );
			}

			return ModContent.GetInstance<Ping>().AveragedPing;
		}
	}
}
