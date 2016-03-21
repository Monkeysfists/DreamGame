using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving player.
    /// </summary>
    public class TrainIdleAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerMoveAnimation.
        /// </summary>
        public TrainIdleAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("trein");
            //FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}
