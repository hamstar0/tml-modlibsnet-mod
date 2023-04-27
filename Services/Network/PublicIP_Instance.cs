using System;
using System.Net;
using Terraria;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.Net;
using ModLibsCore.Services.Timers;


namespace ModLibsNet.Services.Net {
	/// <summary>
	/// Assorted static library functions pertaining to network play.
	/// </summary>
	public partial class PublicIP : ILoadable {
		private string IP = null;



		////////////////

		void ILoadable.OnModsLoad() { }

		void ILoadable.OnPostModsLoad() {
			if( ModLibsNetConfig.Instance.DisableOwnIPCheck ) {
				return;
			}

			this.LoadIPAsync();

			int attempts = 3;

			Timers.SetTimer( 60 * 20, true, delegate () {   // 20 seconds, 3 attempts
				if( this.IP != null ) { // Already acquired?
					return false;
				}

				this.LoadIPAsync();
				return attempts-- > 0;
			} );
		}

		void ILoadable.OnModsUnload() { }


		////////////////

		private void LoadIPAsync() {
			Action<bool, string> onCompletion = null;
			Action<Exception, string> onFail = null;

			onCompletion = ( success, output ) => {
				if( !success ) {
					onFail( new Exception( "Could not reach site." ), output );
					return;
				}
				if( this.IP != null ) {
					return;
				}

				string[] a = output.Split( ':' );
				if( a.Length < 2 ) {
					onFail( new Exception( "Malformed IP output (1)." ), output );
					return;
				}

				string a2 = a[1].Substring( 1 );

				string[] a3 = a2.Split( '<' );
				if( a3.Length == 0 ) {
					onFail( new Exception( "Malformed IP output (2)." ), output );
					return;
				}

				string a4 = a3[0];

				this.IP = a4;
			};

			onFail = ( Exception e, string output ) => {
				if( e is WebException ) {
					LogLibraries.Log( "Could not acquire IP: " + e.Message );
				} else {
					LogLibraries.Alert( "Could not acquire IP: " + e.ToString() );
				}
			};
			
			WebConnectionLibraries.MakeGetRequestAsync( "http://checkip.dyndns.org/", e => onFail(e, ""), onCompletion );
			//NetLibraries.MakeGetRequestAsync( "https://api.ipify.org/", onSuccess, onFail );
			//using( WebClient webClient = new WebClient() ) {
			//	this.PublicIP = webClient.DownloadString( "http://ifconfig.me/ip" );
			//}
		}
	}
}
