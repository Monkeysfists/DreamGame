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
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_joch_lopen@4");
			FrameTime = TimeSpan.FromMilliseconds(100);
		}
	}
}
