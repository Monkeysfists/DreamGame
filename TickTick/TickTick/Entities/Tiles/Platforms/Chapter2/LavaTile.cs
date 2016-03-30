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
    public class LavaTile : AnimatedEntity
    {
        Animation LavaAnimation;
        public LavaTile()
        {
            LavaAnimation = new LavaAnimation();
            Animation = LavaAnimation;
          Size = new Vector2(LevelEntity.TileSize, LevelEntity.TileSize);
            CanCollide = false;
            Visible = true;
        }
    }
}