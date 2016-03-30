using GameLibrary;
using System;

namespace TickTick.Animations
{
    public class LavaAnimation : Animation
    {
        public LavaAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/lava@6");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }

}
