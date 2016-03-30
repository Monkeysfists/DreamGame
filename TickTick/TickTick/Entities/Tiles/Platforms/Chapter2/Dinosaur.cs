using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using TickTick.Animations;

namespace TickTick.Entities.Tiles.Creatures
{
    public class Dinosaur : AnimatedEntity
    {
        public Animation DAnimation;

        public Dinosaur()
        {
            DAnimation = new DinosaurAnimation();
            Animation = DAnimation;
            Visible = true;
            CanCollide = true;
            Size = new Microsoft.Xna.Framework.Vector2(LevelEntity.TileSize * 2, LevelEntity.TileSize * 2);
        }

        public override void Update()
        {
            base.Update();

            if (GameHandler.Random.Next(100) == 0 && DAnimation.FlipHorizontally)
            {
                DAnimation.FlipHorizontally = true;
            }
            else if (GameHandler.Random.Next(100) == 0 && !DAnimation.FlipHorizontally)
            {
                DAnimation.FlipHorizontally = false;
            }
        }
    }
}
