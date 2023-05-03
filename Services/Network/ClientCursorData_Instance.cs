using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;


namespace ModLibsNet.Services.Network {
	/// <summary>
	/// Supplies assorted server informations and tools.
	/// </summary>
	public partial class ClientCursorData {
		internal IDictionary<int, (short X, short Y)> _LastKnownCursorPositions = new Dictionary<int, (short X, short Y)>();



		////////////////

		public override void Load() {
			ClientCursorData.LastKnownCursorPositions
				= new ReadOnlyDictionary<int, (short X, short Y)>( this._LastKnownCursorPositions );
		}
	}
}
