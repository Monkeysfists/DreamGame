using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving teddy.
    /// </summary>
    public class TeddyIdleAnimation : Animation
    {
        /// <summary>
        /// Creates a new TeddyIdleAnimation.
        /// </summary>
        public TeddyIdleAnimation(int chapter)
        {
            switch (chapter)
            {
                case 1: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_teddy_idle"); break;
                case 3: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_teddy_idle"); break;
                default: SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter4/ch4_teddy"); break;
            }
            //FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}
