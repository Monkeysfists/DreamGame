using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// An attacking player.
    /// </summary>
    public class PlayerAttackAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerAttackAnimation.
        /// </summary>
        public PlayerAttackAnimation(int chapter, string item)
        {
            if (chapter == 3)
            {
                switch (item)
                {
                    case "sword": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_sword_attack");
                        FrameTime = TimeSpan.FromMilliseconds(100); break;
                    case "shotgun": SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_shotgun_schieten");
                        FrameTime = TimeSpan.FromMilliseconds(100); break;
                    default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch");
                        FrameTime = TimeSpan.FromMilliseconds(100); break;
                }
            }
            else { return; }
        }
    }
}