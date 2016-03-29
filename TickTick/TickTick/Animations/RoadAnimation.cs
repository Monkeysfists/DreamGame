using GameLibrary;
using System;

namespace TickTick.Animations
{
    class RoadAnimation : Animation
    {
        public RoadAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter3/road_animaiton@3@2");
            FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}
