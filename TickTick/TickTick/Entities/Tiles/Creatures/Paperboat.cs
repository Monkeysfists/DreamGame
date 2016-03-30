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
using TickTick.Entities.Tiles.Platforms.Chapter1;

namespace TickTick.Entities.Tiles.Creatures
{
    class Paperboat : TileEntity
    {

        public Paperboat()
        {
            CanCollide = true;
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/paperboat");
            ResizeToTexture();
            Size /= 1;

        }
    }
}
