using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A rocket moving.
	/// </summary>
	public class RocketMoveAnimation : Animation {
		/// <summary>
		/// Creates a new RocketMoveAnimation.
		/// </summary>
		public RocketMoveAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Rocket/spr_rocket@3");
			FrameTime = TimeSpan.FromMilliseconds(200);
		}
	}
}
