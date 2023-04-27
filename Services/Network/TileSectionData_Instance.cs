using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.Loadable;


namespace ModLibsNet.Services.Network {
	/// <summary>
	/// Supplies tile section data network services (primarily client getter).
	/// </summary>
	public partial class TileSectionData : ILoadable {
		internal IList<TileSectionPacketSubscriber> TileSectionPacketSubs { get; private set; } = new List<TileSectionPacketSubscriber>();



		////////////////

		void ILoadable.OnModsLoad() { }
		void ILoadable.OnModsUnload() { }
		void ILoadable.OnPostModsLoad() { }
	}
}
