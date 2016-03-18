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
    class FlyingCarCreature : CreatureTileEntity
    {
        public int Health
        {
            get
            {
                return _Health;
            }
            set
            {
                if (value < Health)
                {
                    if (Position.Y > Parent.Size.Y)
                    {
                        GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_fall"));
                    }
                    else
                    {
                        GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_die"));
                    }
                }

                _Health = value;

                if (Health <= 0)
                {
                    if (Position.Y <= Parent.Size.Y)
                    {
                        Velocity.Y = -900;
                    }

                    Animation = ExplodeAnimation;
                    ((PlayingState)Parent.Parent).GameOver = true;
                }
            }


        }
        /// <summary>
        /// The right movement speed.
        /// </summary>
        public float RightSpeed;
        /// <summary>
        /// The animation to play when moving right.
        /// </summary>
        public Animation RightAnimation;
        /// <summary>
        /// The animation to play when exploding.
        /// </summary>
        public Animation ExplodeAnimation;
        /// <summary>
        /// Invincible timer control bool
        /// </summary>
        private bool Invin;

        public List<Keys> UpKey;
        public List<Keys> DownKey;
        public float YSpeed;


        private int _Health;
        private bool _Won;
        private float InvinceTimer;
        private float _PreviousY;

        public FlyingCarCreature()
        {
            Health = 6;
            _PreviousY = GlobalCollisionBox.Bottom;
            Name = "player";
            _Won = false;

            // Animations
            IdleAnimation = new PlayerIdleAnimation();
            RightAnimation = new PlayerMoveAnimation();
            Animation = IdleAnimation;

            // Size
            Size = Animation.SpriteSheet.CellSize;
            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;

            // Speed
            RightSpeed = 400F;
            YSpeed = 400F;

            //Timer
            InvinceTimer = 0;

            // Input
            UpKey = new List<Keys>();
            DownKey = new List<Keys>();
            UpKey.Add(Keys.W);
            UpKey.Add(Keys.Up);
            UpKey.Add(Keys.Space);
            DownKey.Add(Keys.S);
            DownKey.Add(Keys.Down);
            DownKey.Add(Keys.LeftShift);
        }

        public override void Update()
        {
            if (Health > 0)
            {
                HandleInput();

                //Update timer if needed
                if (InvinceTimer > 0)
                    InvinceTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

                PlayingState state = ((PlayingState)Parent.Parent);
                //state.Timer.Multiplier = 1;

                // Kill if offscreen
                if (Position.Y > Parent.Size.Y)
                {
                    Health = 0;
                }
            }

            base.Update();

            if (Health > 0)
            {
                HandleCollision();
            }

            if (Health > 0)
            {
                Vector2 bound = (new Vector2(GameHandler.GraphicsHandler.Resolution.X, GameHandler.GraphicsHandler.Resolution.Y) - Size) / 2;
                Vector2 origin = Position - bound;
                Parent.DrawOrigin = new Vector2((int)origin.X, (int)origin.Y);
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void HandleInput()
        {
            if (Animation == ExplodeAnimation)
            {
                Velocity = Vector2.Zero;
            }
            else
            {
                float speedMultiplier = 1F;

                // Movements speed buffs/debuffs

                // Handle input

                if (GameHandler.InputHandler.AnyKeyDown(UpKey))
                {
                    Velocity.Y = -YSpeed * speedMultiplier;
                }
                else if (GameHandler.InputHandler.AnyKeyDown(DownKey))
                {
                    Velocity.Y = YSpeed * speedMultiplier;
                }
            }
        }

        public void HandleCollision()
        {
            if (!_Won)
            {

                Position = new Vector2((float)Math.Floor(Position.X), (float)Math.Floor(Position.Y));

                // Handle collisions
                foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
                {
                    // Win
                    if (entity is GoalTile)
                    {
                        _Won = true;
                        if (_Won)
                        {
                            ((PlayingState)Parent.Parent).Won = true;
                            break;
                        }
                        else if (!(entity is CreatureTileEntity))
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
                                    if (Velocity.Y > 1500)
                                    {
                                        Health -= (int)((Velocity.Y - 1500) / 10);
                                    }
                                Velocity.Y = 0F;
                            }
                            if (!(entity is PlatformTile) || (entity is PlatformTile))
                            {
                                Position.Y += depth.Y + 1;
                                Velocity.Y = 0F;
                            }
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



        public void Explode()
        {
            if (Health > 0)
            {
                Health = 0;
                Velocity = Vector2.Zero;
                Animation = ExplodeAnimation;
            }
        }
    }
    
}
