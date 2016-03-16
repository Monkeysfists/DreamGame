using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving player.
    /// </summary>
    public class TeddyIdleAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerMoveAnimation.
        /// </summary>
        public TeddyIdleAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("");
            //FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}
