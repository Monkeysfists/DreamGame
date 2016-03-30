using GameLibrary;
using System;

namespace TickTick.Animations
{
    public class LavaUnderAnimation : Animation
    {
        public LavaUnderAnimation()
        {

            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/lava_onder@6");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }

    }
}
