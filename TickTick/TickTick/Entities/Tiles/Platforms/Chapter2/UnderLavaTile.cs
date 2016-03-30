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
    public class UnderLavaTile : AnimatedEntity
    {
        Animation LavaUnderAnimation;

        public UnderLavaTile()
        {
            LavaUnderAnimation = new LavaUnderAnimation();
            Animation = LavaUnderAnimation;
            Size = new Vector2(LevelEntity.TileSize, LevelEntity.TileSize);
            CanCollide = false;
            Visible = true;
        }
    }
}
