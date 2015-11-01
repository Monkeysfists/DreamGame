using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A sneezing turtle.
	/// </summary>
	public class TurtleSneezeAnimation : Animation {
		/// <summary>
		/// Creates a new TurtleSneezeAnimation.
		/// </summary>
		public TurtleSneezeAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Turtle/spr_sneeze@9");
			FrameTime = TimeSpan.FromMilliseconds(100);
			Loop = false;
		}
	}
}
