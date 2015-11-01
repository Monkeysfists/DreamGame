using GameLibrary;

namespace TickTick.Entities.Tiles {
	/// <summary>
	/// A goal tile.
	/// </summary>
	public class GoalTile : TileEntity {
		/// <summary>
		/// Creates a new GoalTile.
		/// </summary>
		public GoalTile() {
			Texture = GameHandler.AssetHandler.GetTexture("Sprites/spr_goal");
			Origin.Y = Texture.Height - Size.Y;
			ResizeToTexture();
		}
	}
}
