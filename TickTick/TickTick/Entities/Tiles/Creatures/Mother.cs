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
        public Vector2 BeginSpeed;
        public float SpeedTimer;
        private bool _OnGround;
        private float _PreviousY;

        public Mother()
        {
            CanCollide = false;
            WalkingAnimation = new MotherWalkingAnimation();
            Animation = WalkingAnimation;
            BeginSpeed.X = 100F;

            Size = Animation.SpriteSheet.CellSize * 2;
            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;

            Layer = 3;
            umbrella = new Umbrella();
            //umbrella.Position = Position + new Vector2(0, 100);
            umbrella.Layer = 2;
            this.AddChild(umbrella);
            SpeedTimer = 0;
            Velocity = BeginSpeed;


        }

        public  override void Update()
        {
            Animation = WalkingAnimation;
            //umbrella.Position = this.Position + new Vector2(50,-50);
            //TODO: verschillende snelheden naar rechts lopen
            SpeedTimer += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

            if (SpeedTimer > GameHandler.Random.Next(1, 3))
                ChangeSpeed();

            Console.WriteLine(umbrella.Position.ToString());
            //umbrella.Position.X = Position.X + 100;
            Velocity.Y += 55F;
            Handlecolission();
            base.Update();
        }

        public void ChangeSpeed()
        {
            Velocity = BeginSpeed * (1F + (float)(GameHandler.Random.Next(10, 50) / 100F));
            SpeedTimer = 0;
        }

        public void Handlecolission()
        {
                _OnGround = false;


                Position = new Vector2((float)Math.Floor(Position.X), (float)Math.Floor(Position.Y));

                // Handle collisions
                foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
                {
                    // Win
                   
                        if (!(entity is PlayerCreature))
                        {
                            RectangleF playerBounds = GlobalCollisionBox;
                            RectangleF tileBounds = entity.GlobalCollisionBox;
                            playerBounds.Height++;
                            Vector2 depth = CalculateIntersectionDepth(playerBounds, tileBounds);

                            if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                            {
                                if (!(entity is PlatformTile))
                                {
                                    Position.X += depth.X;
                                }
                            }
                            else
                            {
                                if (_PreviousY - 1 <= tileBounds.Top && Velocity.Y >= 0)
                                {
                                    _OnGround = true;

                                    //if(Velocity.Y > 1500) {
                                    //Health -= (int)((Velocity.Y - 1500) / 50);
                                    //}
                                    Velocity.Y = 0F;
                                }
                                if (!(entity is PlatformTile) || (entity is PlatformTile && _OnGround))
                                {
                                    Position.Y += depth.Y + 1;
                                    Velocity.Y = 0F;
                                }
                                else if (entity is PlatformTile && !_OnGround && Velocity.Y > 0F)
                                {
                                    //Position.Y -= depth.Y - 1;
                                }
                            }
                        }

                    
                }

                _PreviousY = GlobalCollisionBox.Bottom;
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
    }
    }

