using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// An cycling player.
    /// </summary>
    public class PlayerCyclingAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerCyclingAnimation.
        /// </summary>
        public PlayerCyclingAnimation()
        {
           SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/ch3_joch_fiets@4");
           FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}