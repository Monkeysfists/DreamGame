using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms {
	/// <summary>
	/// A hot platform.
	/// </summary>
	public class HotPlatform : PlatformTile {
		/// <summary>
		/// Creates a new HotPlatform.
		/// </summary>
		public HotPlatform() {
			Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_platform_hot");
		}
	}
}
