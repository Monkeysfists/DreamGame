using GameLibrary;

namespace TickTick.Entities.Tiles.Walls {
	/// <summary>
	/// A default wall.
	/// </summary>
	public class DefaultWall : WallTile {
		/// <summary>
		/// Creates a new DefaultWall.
		/// </summary>
		public DefaultWall() {
			Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_wall");
		}
	}
}
