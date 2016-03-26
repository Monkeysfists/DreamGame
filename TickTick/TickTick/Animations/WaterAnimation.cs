using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// Flowing Water
    /// </summary>
    public class WaterAnimation : Animation
    {
        /// <summary>
        /// Creates a new wateranimation
        /// </summary>
        public WaterAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/water");
            FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}