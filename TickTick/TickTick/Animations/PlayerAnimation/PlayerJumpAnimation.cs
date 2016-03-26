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
		public PlayerJumpAnimation(int chapter, string item) {
            switch (chapter)
            {
                case 1: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_joch_springen");
                    FrameTime = TimeSpan.FromMilliseconds(50); break;
                case 2: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/ch2_joch");
                    FrameTime = TimeSpan.FromMilliseconds(50);break;
                case 3: switch (item)
                    {
                        case "shotgun": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_shotgun_springen");
                            FrameTime = TimeSpan.FromMilliseconds(50);break;
                        case "sword": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_sword_springen");
                            FrameTime = TimeSpan.FromMilliseconds(50);break;
                        default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_springen");
                            FrameTime = TimeSpan.FromMilliseconds(50);break;
                    } break;
                default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter4/ch4_joch");
                    FrameTime = TimeSpan.FromMilliseconds(50);break;
            }
            
		}
	}
}
