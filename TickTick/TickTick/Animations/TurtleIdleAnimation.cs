using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// An idle turtle.
	/// </summary>
	public class TurtleIdleAnimation : Animation {
		/// <summary>
		/// Creates a new TurtleIdleAnimation.
		/// </summary>
		public TurtleIdleAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Turtle/spr_idle");
			FrameTime = TimeSpan.FromMilliseconds(100);
		}
	}
}
