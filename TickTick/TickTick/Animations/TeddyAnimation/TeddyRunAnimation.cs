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
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("");
            //FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}
