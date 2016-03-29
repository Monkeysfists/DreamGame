using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// An car player.
    /// </summary>
    public class PlayerCarAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerCarAnimation.
        /// </summary>
        public PlayerCarAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/autorocket@2");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}