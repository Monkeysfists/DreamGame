using GameLibrary;

namespace TickTick.Entities.Tiles.Walls {
	/// <summary>
	/// An ice wall.
	/// </summary>
	public class IceWall : WallTile {
		/// <summary>
		/// Creates a new IceWall.
		/// </summary>
		public IceWall() {
			Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_wall_ice");
		}
	}
}
