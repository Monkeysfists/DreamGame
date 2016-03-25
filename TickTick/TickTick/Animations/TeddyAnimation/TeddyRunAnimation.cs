using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving player.
    /// </summary>
    public class TeddyRunAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerMoveAnimation.
        /// </summary>
        public TeddyRunAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/ch1_teddy_idle");
            //FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}
