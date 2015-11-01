using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// An idle Sparky.
	/// </summary>
	public class SparkyIdleAnimation : Animation {
		/// <summary>
		/// Creates a new SparkyIdleAnimation.
		/// </summary>
		public SparkyIdleAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Sparky/spr_idle");
			FrameTime = TimeSpan.FromMilliseconds(100);
		}
	}
}
