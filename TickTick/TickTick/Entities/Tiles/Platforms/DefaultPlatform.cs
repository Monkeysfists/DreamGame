using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms {
	/// <summary>
	/// A default platform.
	/// </summary>
	public class DefaultPlatform : PlatformTile {
		/// <summary>
		/// Creates a new DefaultPlatform.
		/// </summary>
		public DefaultPlatform() {
			Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_platform");
		}
	}
}
