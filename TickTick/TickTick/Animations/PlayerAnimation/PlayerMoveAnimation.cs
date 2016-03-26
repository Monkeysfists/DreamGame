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
		public PlayerMoveAnimation(int chapter, string item) {
            switch (chapter)
            {
                case 1: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_joch_lopen@4");
                    FrameTime = TimeSpan.FromMilliseconds(100); break;
                case 2: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/ch2_joch");
                    FrameTime = TimeSpan.FromMilliseconds(100); break;
                case 3: switch (item)
                    {
                        case "shotgun": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_shotgun_lopen");
                            FrameTime = TimeSpan.FromMilliseconds(100); break;
                        case "sword": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_sword_lopen");
                            FrameTime = TimeSpan.FromMilliseconds(100); break;
                        default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_lopen");
                            FrameTime = TimeSpan.FromMilliseconds(100); break;
                    } break;
                default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter4/ch4_joch_lopen");
                    FrameTime = TimeSpan.FromMilliseconds(100); break;
            }
		}
	}
}
