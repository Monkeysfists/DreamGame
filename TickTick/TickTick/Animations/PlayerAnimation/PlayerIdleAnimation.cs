using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// An idle player.
	/// </summary>
	public class PlayerIdleAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerIdleAnimation.
		/// </summary>
		public PlayerIdleAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Player/spr_idle");
		}
	}
}
