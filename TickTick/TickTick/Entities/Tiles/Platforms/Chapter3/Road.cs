using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Animations;
using GameLibrary.Types;
using Microsoft.Xna.Framework.Graphics;
namespace TickTick.Entities.Tiles.Platforms.Chapter3
{
    public class Road : PlatformTile
    {
        public Animation RoadAnimation;

        public Road()
        {
            RoadAnimation = new RoadAnimation();
        }


    }
}
