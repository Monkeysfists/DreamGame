using GameLibrary;
using System;

namespace TickTick.Animations
{
    public class GhostAnimation : Animation
    {
        public GhostAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/ghost@4");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
