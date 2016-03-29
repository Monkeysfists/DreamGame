using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// Flowing Water
    /// </summary>
    public class PaperboatAnimation : Animation
    {
        /// <summary>
        /// Creates a new wateranimation
        /// </summary>
        public PaperboatAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/paperboat");
            FrameTime = TimeSpan.FromMilliseconds(200);
        }
    }
}