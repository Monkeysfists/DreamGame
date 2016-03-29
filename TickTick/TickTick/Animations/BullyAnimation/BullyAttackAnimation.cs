using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// An attacking bully.
    /// </summary>
    public class BullyAttackAnimation : Animation
    {
        /// <summary>
        /// Creates a new BullyAttackAnimation.
        /// </summary>
        public BullyAttackAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/pestkop_aanval@4");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
