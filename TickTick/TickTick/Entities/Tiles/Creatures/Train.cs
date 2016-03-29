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
    public class Train : CreatureTileEntity
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
        public bool FULLSTOP;
        public float trainTimer;
        public TextEntity timerText;

        /// <summary>
        /// CHOO CHOO MOTHERUCKER
        /// </summary>
        public Train()
        {
            //timerText = new TextEntity(GameHandler.AssetHandler.GetSpriteFont("Fonts/Hud"));
            //timerText.Layer = 0;
            //timerText.Color = Color.White;
            //AddChild(timerText);

            //Movement
            RightSpeed = 200F;
            Velocity.Y = 0;
            trainTimer = 8;

            CanCollide = true;
            //StartBool = true;

            //Animation
            Rightanimation = new TrainMoveAnimation();
            TrainTexture = new TrainIdleAnimation();
            Animation = TrainTexture;
            //timerText.Text = ((int)trainTimer).ToString();

            // Size
            Size = Animation.SpriteSheet.CellSize;
            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;

        }

        public override void Update()
        {
            //timerText.Position = new Vector2(Parent.Position.X - Position.X + GameHandler.GraphicsHandler.ScreenSize.X / 2.2F, Parent.Position.Y - Position.Y + 25);

            //if (trainTimer > 0)
            //{
            //    trainTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
            //    timerText.Text = ((int)trainTimer).ToString();
            //}


            if (PlayerCreature._OnRails)
            {
                StartBool = true;
                RemoveChild(timerText);
            }

            if (StartBool)
            {
                //TODO: add soundeffect
                Velocity.X = RightSpeed;
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
            //Velocity.X = 0F;
            if (!(FULLSTOP))
            {
                //Position.Y += 0.01F;
                HandleCollision();
            }
            if (FULLSTOP)
                Velocity.X = 0F;

            base.Update();
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

                if (StartBool = false && entity is PlayerCreature)
                    StartBool = true;

                if (!(entity is CreatureTileEntity))
                {
                    RectangleF playerBounds = GlobalCollisionBox;
                    RectangleF tileBounds = entity.GlobalCollisionBox;
                    playerBounds.Height++;
                    Vector2 depth = CalculateIntersectionDepth(playerBounds, tileBounds);

                    if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                    {
                        if (!(entity is TrainTracks))
                        {
                            Position.X += depth.X;
                        }
                    }
                    else
                    {
                        if (_PreviousY - 1 <= tileBounds.Top && Velocity.Y >= 0)
                        {
                            _OnRails = true;
                            Velocity.Y = 0F;
                        }
                        if (!(entity is TrainTracks) || (entity is TrainTracks && _OnRails))
                        {
                            Position.Y += depth.Y + 1;
                            Velocity.Y = 0F;
                        }
                    }
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
            StartBool = false;
            Velocity = Vector2.Zero;
            FULLSTOP = true;
            //TODO: add soundeffect
        }
    }
}
