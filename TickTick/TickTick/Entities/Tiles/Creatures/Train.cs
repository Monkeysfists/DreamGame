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
        /// <summary>
        /// Moving animation of the train
        /// </summary>
        public Animation Rightanimation;
        /// <summary>
        /// The idle train texture
        /// </summary>
        public Animation TrainTexture;

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

            //Animation
            Rightanimation = new TrainMoveAnimation();
            TrainTexture = new TrainIdleAnimation();
            Animation = TrainTexture;
        }

        public override void Update()
        {
            if (StartBool)
            {
                //TODO: add soundeffect
                if (_OnRails)
                    Position += Velocity;
            }
            if (Velocity.X == 400)
            {
                if (!Soundeffectbool)
                {
                    //TODO add soundeffect
                    Soundeffectbool = true;
                }
                if (Animation != Rightanimation)
                    Animation = Rightanimation;
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

      

        public void kill()
        {
            Velocity = new Vector2(0, 0);
            //TODO: add soundeffect
        }
    }
}
