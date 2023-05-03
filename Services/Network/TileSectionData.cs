using System;
using System.IO;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.TModLoader;
using Terraria.ModLoader;

namespace ModLibsNet.Services.Network {
	/// <summary>
	/// Supplies tile section data network services (primarily client getter).
	/// </summary>
	public partial class TileSectionData : ModSystem {
		/// <summary>
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="data"></param>
		public delegate void TileSectionPacketSubscriber( int tileX, int tileY, int width, int height, BinaryReader data );



		////////////////

		/// <summary>
		/// Allows intercepting tile sections as they're received (client only).
		/// </summary>
		/// <param name="callback"></param>
		public static void SubscribeToTileSectionPackets( TileSectionPacketSubscriber callback ) {
			var client = TmlLibraries.SafelyGetInstance<TileSectionData>();

			client.TileSectionPacketSubs.Add( callback );
		}
	}
}
