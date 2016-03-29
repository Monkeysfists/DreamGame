using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using TickTick.Animations;

namespace TickTick.Entities.Tiles.Creatures
{
    public class Dinosaur : CreatureTileEntity
    {
        Animation IdleAnimation;

        public Dinosaur()
        {
            IdleAnimation = new DinosaurAnimation();
        }

        public override void Update()
        {
            if (GameHandler.Random.Next(100) == 0 && IdleAnimation.FlipHorizontally)
            {
                IdleAnimation.FlipHorizontally = true;
            }
            else if (GameHandler.Random.Next(100) == 0 && !IdleAnimation.FlipHorizontally)
            {
                IdleAnimation.FlipHorizontally = false;
            }
        }
    }
}
