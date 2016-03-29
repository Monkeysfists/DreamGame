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
using Microsoft.Xna.Framework.Input;

namespace TickTick.Entities.Tiles.Platforms
{
    public class CollisionObject : TileEntity
    {
        public CollisionObject(string name, Vector2 size)
        {
            Texture = GameHandler.AssetHandler.GetTexture(name);
            CanCollide = true;
            Size = size;
        }
    }
}
