using GameLibrary;

namespace TickTick.Entities.Tiles.Walls {
	/// <summary>
	/// A hot wall.
	/// </summary>
	public class HotWall : WallTile {
		/// <summary>
		/// Creates a new HotWall.
		/// </summary>
		public HotWall() {
			Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_wall_hot");
		}
	}
}
