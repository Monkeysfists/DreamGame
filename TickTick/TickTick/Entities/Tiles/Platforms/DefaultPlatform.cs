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
			Texture = GameHandler.AssetHandler.GetTexture("stoep");
            Size = new Microsoft.Xna.Framework.Vector2(20,20);
		}
	}
}
