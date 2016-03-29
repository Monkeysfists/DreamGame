using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles.Platforms {
	/// <summary>
	/// A default platform.
	/// </summary>
	public class DefaultPlatform : PlatformTile {
		/// <summary>
		/// Creates a new DefaultPlatform.
		/// </summary>
		public DefaultPlatform(Vector2 size) {
			Texture = GameHandler.AssetHandler.GetTexture("chapter2/wooden_platform");
            Size = size;
		}
	}
}
