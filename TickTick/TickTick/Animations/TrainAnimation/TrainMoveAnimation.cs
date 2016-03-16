using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving player.
    /// </summary>
    public class TrainMoveAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerMoveAnimation.
        /// </summary>
        public TrainMoveAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("");
            FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}
