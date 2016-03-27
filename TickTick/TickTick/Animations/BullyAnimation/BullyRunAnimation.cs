using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A running bully.
    /// </summary>
    public class BullyRunAnimation : Animation
    {
        /// <summary>
        /// Creates a new BullyRunAnimation.
        /// </summary>
        public BullyRunAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/pestkop_lopen");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
