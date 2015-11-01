using GameLibrary;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// Typically an enemy or player.
	/// </summary>
	public class CreatureTileEntity : AnimatedEntity {
		/// <summary>
		/// The animation to play when idle.
		/// </summary>
		public Animation IdleAnimation;

		/// <summary>
		/// Creates a new CreatureTileEntity.
		/// </summary>
		public CreatureTileEntity() {
			CanCollide = true;
		}
	}
}
