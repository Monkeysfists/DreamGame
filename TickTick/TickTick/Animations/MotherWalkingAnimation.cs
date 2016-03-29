using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A walking mother
    /// </summary>
    public class MotherWalkingAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerDieAnimation.
        /// </summary>
        public MotherWalkingAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/mother_walking@2");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
