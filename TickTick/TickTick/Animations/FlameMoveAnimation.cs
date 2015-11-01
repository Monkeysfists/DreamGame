using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A flame moving.
	/// </summary>
	public class FlameMoveAnimation : Animation {
		/// <summary>
		/// Creates a new FlameMoveAnimation.
		/// </summary>
		public FlameMoveAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Flame/spr_flame@9");
			FrameTime = TimeSpan.FromMilliseconds(100);
		}
	}
}
