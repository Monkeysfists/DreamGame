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
using Microsoft.Xna.Framework.Input;

namespace TickTick.Entities.Tiles.Creatures
{
    public class TeddyBear : CreatureTileEntity
    {
        private Animation TeddyIdleAnimation;
        private Animation TeddyRunAnimation;
        private Animation TeddyAttackAnimation;
        private Animation TeddyTalkAnimation;

        private float Health;
        private float Speed;

        private Vector2 PlayerPosition;

        private float IntervalTimer;
        private bool canAttack;

        private bool mirrored;

        List<Entity> EntityList;
        PlayerCreature player;

        private bool _OnGround;
        private float _PreviousY;

        public int Chapter;

        public HintEntity TalkBox;
        public float TalkStage;



        public TeddyBear(int chapter)
        {
            Health = 100;
            Name = "TeddyBear";
            CanCollide = true;
            _PreviousY = GlobalCollisionBox.Bottom;
            Visible = true;
            this.Chapter = chapter;
            TalkBox = new HintEntity();
            TalkBox.Visible = false;
            TalkStage = 0;
            TalkBox.Text = "Loop met WASD en spring met spatie";
            AddChild(TalkBox);
            TalkBox.SetTextPosition = TalkBox.SetTextPosition + new Vector2(0,25);


            // Speed
            Speed = 200F;


            //Timer
            IntervalTimer = 1F;

            //Animations
            TeddyIdleAnimation = new TeddyIdleAnimation(chapter);
            TeddyRunAnimation = new TeddyRunAnimation();
            TeddyAttackAnimation = new TeddyAttackAnimation();
            TeddyTalkAnimation = new TeddyTalkingAnimation();
            if(chapter == 1)
                Animation = TeddyTalkAnimation;
            if(Chapter != 1)
                Animation = TeddyIdleAnimation;

            // Size
            Size = Animation.SpriteSheet.CellSize;
            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;


        }

        public override void Update()
        {
            // Animation directions
            if (Velocity.X > 0)
            {
                IdleAnimation.FlipHorizontally = false;
            }
            else if (Velocity.X < 0)
            {
                IdleAnimation.FlipHorizontally = true;
            }

            if(IntervalTimer > 0)
            {
                canAttack = false;
            }else if(IntervalTimer <= 0)
            {
                canAttack = true;
            }

            if(Chapter == 1)
                TalkStage += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

            if (TalkStage > 3 && TalkStage < 6)
                TalkBox.Text = "Je kan hoger springen door op het bed een sprong te maken!";

            if (TalkStage > 9)
            {
                RemoveChild(TalkBox);
                Animation = TeddyIdleAnimation;
            }
            /*
            if (player.Position.X > Position.X)
                mirrored = true;
            if (IntervalTimer == 0 && Visible)
                GetPlayer();
            if (!mirrored && Position.X - player.Position.X > 20 && IntervalTimer == 0)
                Position.X -= Speed;
            else if (mirrored && player.Position.X - Position.X > 20 && IntervalTimer == 0)
                Position.X += Speed;

            if(MathHelper.Distance(player.Position.X, Position.X) < 20)
            {
                //Attack();
                IntervalTimer += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
            }

            if (IntervalTimer > 2)
                IntervalTimer = 0;
                */

            // Handle physics
            if (Animation != TeddyAttackAnimation)
            {
                Velocity.Y += 55F; // Fall speed
            }

            if (Health > 0)
                HandleCollision();


            HandleInput();
            base.Update();

            TalkBox.Position = Parent.Position - new Vector2(100F,100F);
            //TalkBox.SetTextPosition = Par.Position - new Vector2(90F, 90F);
            TalkBox.Visible = true;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void HandleInput()
        {
        }

        public void HandleCollision()
        {

                _OnGround = false;


                Position = new Vector2((float)Math.Floor(Position.X), (float)Math.Floor(Position.Y));

                // Handle collisions
                foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
                {
                if (entity is PlayerCreature)
                {
                    if (Chapter == 3)
                        Animation = TeddyAttackAnimation;
                }
                if (!(entity is CreatureTileEntity) || (entity is Train))
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
                            if (!(entity is PlatformTile) || (entity is PlatformTile && _OnGround) || entity is TrampolineBedTile)
                            {
                                Position.Y += depth.Y + 1;
                                Velocity.Y = 0F;
                            }
                            else if (entity is PlatformTile && !_OnGround && Velocity.Y > 0F)
                            {
                                Position.Y -= depth.Y - 1;
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

        public bool CanAttack
        {
            get { return canAttack; }
        }

        public float GetHealth
        {
            get { return Health; }
        }

        public void Attack()
        {
            //if()
        }

    }
}
