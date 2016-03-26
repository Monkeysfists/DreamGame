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


namespace TickTick.Entities.Tiles.Creatures
{
    class Mother : CreatureTileEntity
    {
        public Animation WalkingAnimation;
        public Umbrella umbrella;

        public Mother()
        {
            CanCollide = false;
            WalkingAnimation = new MotherWalkingAnimation();
            umbrella = new Umbrella();
            this.AddChild(umbrella);
        }

        private void Update()
        {
            Animation = WalkingAnimation;
            umbrella.Position = this.Position + new Vector2(50,-50);
            //TODO: verschillende snelheden naar rechts lopen
        }
    }
}
