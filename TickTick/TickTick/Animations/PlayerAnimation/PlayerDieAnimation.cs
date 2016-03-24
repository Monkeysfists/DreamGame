using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// A dying player.
	/// </summary>
	public class PlayerDieAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerDieAnimation.
		/// </summary>
		public PlayerDieAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_joch_bukken");
			FrameTime = TimeSpan.FromMilliseconds(50);
		}
	}
}
