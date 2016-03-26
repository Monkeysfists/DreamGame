using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving teddy.
    /// </summary>
    public class TeddyRunAnimation : Animation
    {
        /// <summary>
        /// Creates a new TeddyRunAnimation.
        /// </summary>
        public TeddyRunAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_teddy_walking");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
