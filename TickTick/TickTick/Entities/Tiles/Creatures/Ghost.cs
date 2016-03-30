using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.States;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Creatures
{
    public class Ghost : CreatureTileEntity
    {
        public Animation WalkAnimation;
        public bool Moving;
        private float Timer;

        public Ghost()
        {
            WalkAnimation = new GhostAnimation();
            Animation = WalkAnimation;
            Moving = false;
            Visible = false;
            Size = new Vector2(150,280);
            CanCollide = true;
        }

        public override void Update()
        {
            Timer += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

            if (Moving)
                    Position.X += 0.5f;

            if (Timer > 2 && !Moving)
            {
                Moving = true;
                Visible = true;
            }

            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is GhostStop)
                {
                    Moving = false;
                }
            }

            base.Update();
        }
    }
}
