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
    public class WaterTile : AnimatedEntity
    {
        public Animation waterAnimation;

        public WaterTile()
        {
            waterAnimation = new WaterAnimation();
            Animation = waterAnimation;
            Visible = true;
            //CanCollide = true;
            Velocity = Vector2.Zero;
            // Size
            Size = Animation.SpriteSheet.CellSize * 2;
            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;
        }

        public override void Update()
        {
            //this.Animation = waterAnimation;
            base.Update();
        }
    }
}