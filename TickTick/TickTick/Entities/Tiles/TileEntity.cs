using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles {
	/// <summary>
	/// A default tile entity, with a size of 72x55.
	/// </summary>
	public class TileEntity : TextureEntity {
		/// <summary>
		/// Creates a new TileEntity.
		/// </summary>
		public TileEntity() {
			Size = new Vector2(1000, 500);
			CanCollide = true;
        }
	}
}
