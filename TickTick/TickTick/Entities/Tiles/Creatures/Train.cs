using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using TickTick.Entities.Tiles.Platforms;
using GameLibrary.Types;

namespace TickTick.Entities.Tiles.Creatures
{
    class Train : CreatureTileEntity
    {
        /// <summary>
        /// The right movement speed.
        /// </summary>
        public float RightSpeed;
        /// <summary>
        /// Check if train should start
        /// </summary>
        public bool StartBool;
        /// <summary>
        /// Check if train is on his rails
        /// </summary>
        private bool _OnRails;
        /// <summary>
        /// Check if soundeffect has played already
        /// </summary>
        private bool Soundeffectbool;

        private float _PreviousY;

        /// <summary>
        /// CHOO CHOO MOTHERUCKER
        /// </summary>
        public Train()
        {
            // Size
            Size = Animation.SpriteSheet.CellSize;
            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;

            //Movement
            RightSpeed = 400F;
            Velocity.Y = 0;
        }

        public override void Update()
        {
            if (StartBool)
            {
                //TODO: add soundeffect
                if (_OnRails)
                    Position += Velocity;
            }
            if(Velocity.X == 400)
                if (!Soundeffectbool)
                {
                    //TODO add soundeffect
                    Soundeffectbool = true;
                }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void HandleCollision()
        {
            _OnRails = false;
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is CreatureTileEntity && _OnRails)
                {
                    StartBool = true;
                    Velocity.X = RightSpeed;
                }
                if(entity is TrainStopBlock)
                {
                    kill();
                }
            }
        }

        public Vector2 CalculateIntersectionDepth(Rectangle rectangle1, Rectangle rectangle2)
        {
            Vector2 center1 = new Vector2(rectangle1.Center.X, rectangle1.Center.Y);
            Vector2 center2 = new Vector2(rectangle2.Center.X, rectangle2.Center.Y);
            Vector2 minDistance = new Vector2(rectangle1.Width + rectangle2.Width, rectangle1.Height + rectangle2.Height) / 2;
            Vector2 distance = center1 - center2;
            Vector2 depth = Vector2.Zero;
            if (distance.X > 0)
            {
                depth.X = minDistance.X - distance.X;
            }
            else
            {
                depth.X = -minDistance.X - distance.X;
            }
            if (distance.Y > 0)
            {
                depth.Y = minDistance.Y - distance.Y;
            }
            else
            {
                depth.Y = -minDistance.Y - distance.Y;
            }
            return depth;
        }

        public void kill()
        {
            Velocity = new Vector2(0, 0);
            //TODO: add soundeffect
        }
    }
}
