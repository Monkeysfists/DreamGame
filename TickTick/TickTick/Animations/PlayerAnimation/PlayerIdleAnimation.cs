using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// An idle player.
	/// </summary>
	public class PlayerIdleAnimation : Animation {
		/// <summary>
		/// Creates a new PlayerIdleAnimation.
		/// </summary>
		public PlayerIdleAnimation(int chapter, string item) {
            switch (chapter)
            {
                case 1:
                case 2:
                    SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_joch@1"); break;
                case 3:
                case 4:
                    SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/ch2_joch"); break;
                case 5:
                case 8: switch (item)
                    {
                        case "shotgun": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_shotgun"); break;
                        case "sword": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_sword_idle"); break;
                        default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch"); break;
                    } break;
                default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter4/ch4_joch"); break;
            }
            
		}
	}
}
