using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms {
	/// <summary>
	/// An ice platform.
	/// </summary>
	public class IcePlatform : PlatformTile {
		/// <summary>
		/// Creates a new IcePlatform.
		/// </summary>
		public IcePlatform() {
			Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_platform_ice");
		}
	}
}
