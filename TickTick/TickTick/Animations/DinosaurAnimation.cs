using GameLibrary;
using System;

namespace TickTick.Animations
{
    public class DinosaurAnimation : Animation
    {
        public DinosaurAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/dinosaur@2");
            FrameTime = TimeSpan.FromMilliseconds(100);
        }
    }
}
