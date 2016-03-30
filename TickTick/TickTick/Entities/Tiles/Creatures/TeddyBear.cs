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
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Creatures
{
    public class TeddyBear : CreatureTileEntity
    {
        private Animation TeddyIdleAnimationRight;
        private Animation TeddyIdleAnimationLeft;
        private Animation TeddyRunAnimation;
        private Animation TeddyAttackAnimation;
        private Animation TeddyTalkAnimation;

        private float Health;
        private float Speed;

        private Vector2 PlayerPosition;

        private float IntervalTimer;
        public static bool canAttack;

        private bool mirrored;

        List<Entity> EntityList;
        playerCreature player;

        private bool _OnGround;
        private float _PreviousY;

        public int Chapter;

        public HintEntity TalkBox;
        public float TalkStage;


        public TeddyBear(int chapter)
        {
            Health = 5;
            Name = "TeddyBear";
            if (chapter == 5)
                CanCollide = true;
            else { CanCollide = false; }
            _PreviousY = GlobalCollisionBox.Bottom;
            Visible = true;
            this.Chapter = chapter;
            TalkBox = new HintEntity();
            TalkBox.Visible = false;
            TalkStage = 0;
            TalkBox.Text = "Loop met WASD en spring met spatie";
            //AddChild(TalkBox);
            //TalkBox.SetTextPosition = TalkBox.SetTextPosition + new Vector2(0,25);


            // Speed
            Speed = 700F;


            //Timer
            IntervalTimer = 1F;

            //Animations
            TeddyIdleAnimationRight = new TeddyIdleAnimation(chapter);
            TeddyIdleAnimationLeft = new TeddyIdleAnimation(chapter);
            TeddyIdleAnimationLeft.FlipHorizontally = true;
            TeddyRunAnimation = new TeddyRunAnimation();
            TeddyAttackAnimation = new TeddyAttackAnimation();
            TeddyTalkAnimation = new TeddyTalkingAnimation();
            Animation = TeddyIdleAnimationRight;
            if (chapter == 1)
                Animation = TeddyTalkAnimation;


            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;

            if (chapter == 5)
                Velocity.X = Speed;


        }

        public override void Update()
        {

            if(GameHandler.Random.Next(0,100) <= 7)
            {
                Velocity.X *= -1;
            }
            // Animation directions
            if (Velocity.X > 0)
            {
                Animation = TeddyIdleAnimationLeft;
            }
            else if (Velocity.X < 0)
            {
                Animation = TeddyIdleAnimationRight;
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

            if (Chapter == 1)
                AddChild(TalkBox);

            if(Chapter != 1)
                RemoveChild(TalkBox);

            if (TalkStage > 3 && TalkStage < 6)
                TalkBox.Text = "Je kan hoger springen door op het bed een sprong te maken!";

            if (TalkStage > 9)
            {
                RemoveChild(TalkBox);
                Animation = TeddyIdleAnimationRight;
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

            // Size
            switch (Chapter)
            {
                case 1: Size = new Vector2(LevelEntity.TileSize * 2, LevelEntity.TileSize * 4); break;
                case 3: Size = new Vector2(LevelEntity.TileSize * 2, LevelEntity.TileSize * 2); break;
                case 5: Size = Animation.SpriteSheet.CellSize / 2; break;
                default: Size = Animation.SpriteSheet.CellSize; break;
            }
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
                if (entity is playerCreature)
                {
                    if (Chapter == 5)
                    {
                        Animation = TeddyAttackAnimation;
                        GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/bear"));
                    }
                }

                else 
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
                            else if (entity is Bullet)
                            {
                                Health -= 1;
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
