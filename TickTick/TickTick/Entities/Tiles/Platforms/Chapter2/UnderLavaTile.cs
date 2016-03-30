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

namespace TickTick.Entities.Tiles.Platforms
{
    public class UnderLavaTile : PlatformTile
    {

        public UnderLavaTile()
        {
            GameHandler.AssetHandler.GetSpriteSheet("chapter2/lava_onder@6");
            Size = new Vector2(LevelEntity.TileSize, LevelEntity.TileSize);

        }
    }
}
