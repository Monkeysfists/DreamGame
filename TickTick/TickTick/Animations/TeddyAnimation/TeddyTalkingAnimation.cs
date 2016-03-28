using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving teddy.
    /// </summary>
    public class TeddyTalkingAnimation : Animation
    {
        /// <summary>
        /// Creates a new TeddyAttackAnimation.
        /// </summary>
        public TeddyTalkingAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_teddy_talking@2");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
