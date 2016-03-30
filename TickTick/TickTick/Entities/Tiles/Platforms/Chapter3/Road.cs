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

        public Road()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter3/road_animation@3@2");
            Size.X *= 100;
            Size.Y *= 30;
            CanCollide = false;
        }


    }
}
