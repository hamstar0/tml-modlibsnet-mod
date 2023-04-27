using System;
using Terraria;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;


namespace ModLibsNet {
	/// @private
	partial class ModLibsNetMod : Mod {
		public static ModLibsNetMod Instance { get; private set; }



		////////////////

		public override void Load() {
			ModLibsNetMod.Instance = this;
		}

		////

		public override void Unload() {
			try {
				LogLibraries.Alert( "Unloading mod..." );
			} catch { }

			ModLibsNetMod.Instance = null;
		}
	}
}
