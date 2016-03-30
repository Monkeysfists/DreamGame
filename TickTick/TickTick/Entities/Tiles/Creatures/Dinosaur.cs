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
        Animation DAnimation;

        public Dinosaur()
        {
            DAnimation = new DinosaurAnimation();
            Animation = DAnimation;
            Visible = true;
            CanCollide = false;
        }

        public override void Update()
        {
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
