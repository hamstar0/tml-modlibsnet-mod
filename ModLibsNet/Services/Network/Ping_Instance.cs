using System;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;


namespace ModLibsNet.Services.Net {
	/// <summary>
	/// Assorted static library functions pertaining to network play.
	/// </summary>
	public partial class Ping : ILoadable {
		private int AveragedPing = -1;
		private int CurrentPing = -1;



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		internal void UpdatePing( int upTimeSpan, int downTimeSpan, int totalSpan ) {
			if( this.AveragedPing < 0 ) {
				this.AveragedPing = totalSpan;
			} else {
				//this.CurrentPing = ( this.CurrentPing + this.CurrentPing + ping ) / 3;
				this.AveragedPing = ( this.AveragedPing + totalSpan ) / 2;
			}

			this.CurrentPing = totalSpan;
		}
	}
}
