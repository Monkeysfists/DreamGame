using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A jumping player.
	/// </summary>
	public class PlayerJumpAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerJumpAnimation.
		/// </summary>
		public PlayerJumpAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Player/spr_jump@14");
            FrameTime = TimeSpan.FromMilliseconds(50);
		}
	}
}
