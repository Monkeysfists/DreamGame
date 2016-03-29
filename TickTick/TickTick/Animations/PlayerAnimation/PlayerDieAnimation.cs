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
		public PlayerDieAnimation(int chapter) {
            switch (chapter)
            {
                case 1: case 2: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_joch_die@2");
                    FrameTime = TimeSpan.FromMilliseconds(50); break;
                case 3: case 4: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/ch2_joch_die@7");
                    FrameTime = TimeSpan.FromMilliseconds(50);break;
                case 5: case 6: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_die@3");
                    FrameTime = TimeSpan.FromMilliseconds(50);break;
                default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter4/ch4_joch");
                    FrameTime = TimeSpan.FromMilliseconds(50);break;
            }
		}
	}
}
