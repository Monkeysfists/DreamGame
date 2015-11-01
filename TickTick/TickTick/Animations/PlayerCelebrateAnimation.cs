using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A celebrating player.
	/// </summary>
	public class PlayerCelebrateAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerCelebrateAnimation.
		/// </summary>
		public PlayerCelebrateAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Player/spr_celebrate@14");
			FrameTime = TimeSpan.FromMilliseconds(50);
		}
	}
}
