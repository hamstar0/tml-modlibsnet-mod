using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using ModLibsCore.Classes.Errors;


namespace ModLibsNet.Services.Network {
	/// <summary>
	/// Supplies tile section data network services (primarily client getter).
	/// </summary>
	public partial class TileSectionData {
		internal IList<TileSectionPacketSubscriber> TileSectionPacketSubs { get; private set; } = new List<TileSectionPacketSubscriber>();
	}
}
