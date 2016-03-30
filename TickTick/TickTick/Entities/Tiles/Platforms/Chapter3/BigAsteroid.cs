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
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Platforms
{
    public class BigAsteroid : PlatformTile
    {

        public BigAsteroid()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter3/asteroide" + GameHandler.Random.Next(1, 4));
            Size *= 4;
            CanCollide = true;
        }

    }
}
