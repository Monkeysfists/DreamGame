using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// An idle bully.
    /// </summary>
    public class BullyIdleAnimation : Animation
    {
        /// <summary>
        /// Creates a new BullyIdleAnimation.
        /// </summary>
        public BullyIdleAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/pestkop");
            FrameTime = TimeSpan.FromMilliseconds(100);
            //_aanval _lopen
        }
    }
}
