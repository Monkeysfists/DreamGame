using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// An exploding player.
	/// </summary>
	public class PlayerExplodeAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerExplodeAnimation.
		/// </summary>
		public PlayerExplodeAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Player/spr_explode@5x5");
			FrameTime = TimeSpan.FromMilliseconds(40);
			Loop = false;
		}
	}
}
