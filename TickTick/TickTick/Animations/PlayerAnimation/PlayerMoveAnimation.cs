using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A moving player.
	/// </summary>
	public class PlayerMoveAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerMoveAnimation.
		/// </summary>
		public PlayerMoveAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Player/spr_run@13");
			FrameTime = TimeSpan.FromMilliseconds(50);
		}
	}
}
