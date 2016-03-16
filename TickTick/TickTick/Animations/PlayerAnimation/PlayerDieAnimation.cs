using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A dying player.
	/// </summary>
	public class PlayerDieAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerDieAnimation.
		/// </summary>
		public PlayerDieAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Player/spr_die@5");
			FrameTime = TimeSpan.FromMilliseconds(50);
		}
	}
}
