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
	public partial class PublicIP {
		/// <summary>
		/// Gets the internet-facing IP address of the current machine.
		/// </summary>
		/// <returns></returns>
		public static string GetPublicIP() {
			var netLib = ModContent.GetInstance<PublicIP>();
			if( netLib?.IP == null ) {
				throw new ModLibsException( "Public IP not yet acquired." );
			}

			return netLib.IP;
		}
	}
}
