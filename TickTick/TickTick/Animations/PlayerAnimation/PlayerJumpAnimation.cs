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
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_joch_springen");
            FrameTime = TimeSpan.FromMilliseconds(50);
		}
	}
}
