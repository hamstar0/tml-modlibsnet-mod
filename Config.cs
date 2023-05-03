using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace ModLibsNet {
	/// <summary>
	/// Defines config settings.
	/// </summary>
	[Label( "Mod Libs - Services - Net - Settings" )]
	public class ModLibsNetConfig : ModConfig {
		/// <summary>
		/// Gets the stack-merged singleton instance of this config file.
		/// </summary>
		public static ModLibsNetConfig Instance => ModContent.GetInstance<ModLibsNetConfig>();



		////////////////

		/// @private
		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		/// <summary>
		/// Clients occasionally ping server to gauge their latency.
		/// </summary>
		[Label( "Are Clients Gauging Average Ping" )]
		[Tooltip( "Client occasionally pings server to guage their latency." )]
		[DefaultValue( true )]
		public bool IsClientsGaugingAveragePing { get; set; } = true;

		/// <summary>
		/// Duration between latency pings per client.
		/// </summary>
		[Label( "Ping Update Delay" )]
		[Tooltip( "Duration between latency pings per client." )]
		[Range( 2, 60 * 120 )]
		[DefaultValue( 60 * 15 )]
		public int PingUpdateDelay { get; set; } = 60 * 15;   // 15 seconds

		////

		/// <summary>
		/// Disables IP address checks from checkip.dyndns.org.
		/// </summary>
		[Label( "Disable self IP checking" )]
		[Tooltip( "Skips calls to checkip.dyndns.org for getting own IP" )]
		[DefaultValue( true )]
		[ReloadRequired]
		public bool DisableOwnIPCheck { get; set; } = true;
	}
}
