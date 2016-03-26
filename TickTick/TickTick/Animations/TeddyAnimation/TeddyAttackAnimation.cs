using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving teddy.
    /// </summary>
    public class TeddyAttackAnimation : Animation
    {
        /// <summary>
        /// Creates a new TeddyAttackAnimation.
        /// </summary>
        public TeddyAttackAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_teddy_aanval");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
