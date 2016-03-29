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
using TickTick.Entities.Tiles.Platforms.Chapter1;
using TickTick.Entities.Tiles.Platforms.Chapter3;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// A controllable player creature.
	/// </summary>
	public class PlayerCreature : CreatureTileEntity {
		public int Health {
			get {
				return _Health;
			}
			set {
                _Health = value;

				if (Health <= 0) {
					Animation = DieAnimation;
                    Velocity = Vector2.Zero;
					((PlayingState)Parent.Parent).GameOver = true;
				}
			}
		}
		/// <summary>
		/// The keys to move left.
		/// </summary>
		public List<Keys> LeftKey;
		/// <summary>
		/// The keys to move right.
		/// </summary>
		public List<Keys> RightKey;
		/// <summary>
		/// The keys to jump.
		/// </summary>
		public List<Keys> JumpKey;
        public List<Keys> CrouchKey;
        public List<Keys> AttackKey;
		/// <summary>
		/// The left movement speed.
		/// </summary>
		public float LeftSpeed;
		/// <summary>
		/// The right movement speed.
		/// </summary>
		public float RightSpeed;
		/// <summary>
		/// The jump movement speed.
		/// </summary>
		public float JumpSpeed;
		/// <summary>
		/// The animation to play when moving left.
		/// </summary>
		public Animation LeftAnimation;
		/// <summary>
		/// The animation to play when moving right.
		/// </summary>
		public Animation RightAnimation;
		/// <summary>
		/// The animation to play when jumping.
		/// </summary>
		public Animation JumpAnimation;
		/// <summary>
		/// The animation to play when celebrating.
		/// </summary>
		public Animation CelebrateAnimation;
		/// <summary>
		/// The animation to play when dying.
		/// </summary>
		public Animation DieAnimation;
		/// <summary>
		/// The animation to play when exploding.
		/// </summary>
		public Animation ExplodeAnimation;

        public Animation CrouchAnimation;

        public Animation AttackAnimation;

        public Animation CarAnimation;

        public Animation CyclingAnimation;


        /// <summary>
        /// Invincible timer control bool
        /// </summary>
        private bool Invin;
        public static bool _OnRails;

		private int _Health;
		private bool _OnGround;
		private float _PreviousY;
		private bool _Won;
        private int TemporaryHealth;
        private float AttackTimer;
        private float InvinceTimer;
        private float KnockbackTimer;
        public static string item;
        private int chapter;
        public float trainTimer;
        public bool Trampo;
        public Vector2 TrainSpeed;
        private float _Timer;
        private int lane;

		/// <summary>
		/// Creates a new PlayerCreature.
		/// </summary>
		public PlayerCreature(int chapter) {
			Health = 6;
			_PreviousY = GlobalCollisionBox.Bottom;
			Name = "player";
			_Won = false;
            CanCollide = true;
            this.chapter = chapter;
            InvinceTimer = 0;
            KnockbackTimer = 0;
            NewAnimations = false;
            item = "";

			// Animations
			IdleAnimation = new PlayerIdleAnimation(chapter, item);
			LeftAnimation = new PlayerMoveAnimation(chapter, item);
			LeftAnimation.FlipHorizontally = true;
			RightAnimation = new PlayerMoveAnimation(chapter, item);
			JumpAnimation = new PlayerJumpAnimation(chapter, item);
			DieAnimation = new PlayerDieAnimation(chapter);
            CrouchAnimation = new PlayerCrouchAnimation();
            AttackAnimation = new PlayerAttackAnimation(chapter, item);
            CyclingAnimation = new PlayerCyclingAnimation();
            CarAnimation = new PlayerCarAnimation();
			Animation = IdleAnimation;

			// Size
			Size = Animation.SpriteSheet.CellSize * 2;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2;

			// Speed
			LeftSpeed = -200F;
			RightSpeed = 200F;
			JumpSpeed = -1100F;

            //Timer
            InvinceTimer = 0;
            trainTimer = 8;

			// Input
			LeftKey = new List<Keys>();
			RightKey = new List<Keys>();
			JumpKey = new List<Keys>();
            CrouchKey = new List<Keys>();
            AttackKey = new List<Keys>();
			LeftKey.Add(Keys.A);
			LeftKey.Add(Keys.Left);
			RightKey.Add(Keys.D);
			RightKey.Add(Keys.Right);
			JumpKey.Add(Keys.W);
			JumpKey.Add(Keys.Up);
			JumpKey.Add(Keys.Space);
            CrouchKey.Add(Keys.S);
            CrouchKey.Add(Keys.Down);
            AttackKey.Add(Keys.P);
            AttackKey.Add(Keys.F);
		}

        public void LoadAnimations()
        {
            IdleAnimation = new PlayerIdleAnimation(chapter, item);
            LeftAnimation = new PlayerMoveAnimation(chapter, item);
            LeftAnimation.FlipHorizontally = true;
            RightAnimation = new PlayerMoveAnimation(chapter, item);
            JumpAnimation = new PlayerJumpAnimation(chapter, item);
            AttackAnimation = new PlayerAttackAnimation(chapter, item);
            Animation = IdleAnimation;
            Size = Animation.SpriteSheet.CellSize * 2;
        }

        bool NewAnimations;

		public override void Update() {

            if (Health > 0 ) {
                if (_OnRails) ;
                    //Velocity.X = RightSpeed;

                if ((item == "shotgun" || item == "sword") && !NewAnimations && (chapter == 5 || chapter == 8))
                {
                    LoadAnimations();
                    NewAnimations = true;
                }

                //Update timer if needed
                if (InvinceTimer > 0F)
                {
                    InvinceTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                }
                if (AttackTimer > 0) {
                    AttackTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                }
                if (trainTimer > 0)
                    trainTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                if (KnockbackTimer > 0)
                    KnockbackTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

                if (KnockbackTimer > 0.80F && KnockbackTimer < 0.9F)
                    Jump(JumpSpeed * 0.3F);

                if (KnockbackTimer > 0.4F && KnockbackTimer < 0.5F)
                    Velocity = Vector2.Zero;

                if (Animation != CelebrateAnimation)
                {
					// Animation directions
                    if (Velocity.X > 0)
                    {
						IdleAnimation.FlipHorizontally = false;
						JumpAnimation.FlipHorizontally = false;
                        CrouchAnimation.FlipHorizontally = false;
                    }
                    else if (Velocity.X < 0)
                    {
						IdleAnimation.FlipHorizontally = true;
						JumpAnimation.FlipHorizontally = true;
                        CrouchAnimation.FlipHorizontally = true;
					}



					// Set the correct animation
                    if (chapter != 6 && chapter != 7)
                    {
                        if (_OnGround)
                        {
                            if (Velocity.X == 0)
                                Animation = IdleAnimation;
                            else
                            {
                                if (Velocity.X > 0)
                                {
                                    Animation = RightAnimation;
                                }
                                else if (Velocity.X < 0)
                                {
                                    Animation = LeftAnimation;
                                }
                            }
                        }
                        else if (Velocity.Y != 0)
                        {
                            Animation = JumpAnimation;
                        }
                        if (Velocity.X == 0 && Animation != CrouchAnimation)
                            Animation = IdleAnimation;
                    }
                    else if (chapter == 6)
                        CarLevel();
                    else if (chapter == 7)
                        BikeLevel();
                }

				PlayingState state = ((PlayingState)Parent.Parent);
					//state.Timer.Multiplier = 1;

				// Kill if offscreen
				if (Position.Y > Parent.Size.Y) {
					Health = 0;
				}
			}

            //Velocity.Y = 0F;
            if (GameHandler.InputHandler.AnyKeyDown(CrouchKey) && (chapter == 1 || chapter == 2))
                Animation = CrouchAnimation;
			base.Update();

			// Handle physics
			if (Animation != ExplodeAnimation) {
				Velocity.Y += 55F; // Fall speed
			}



			if (Health > 0) {
				Vector2 bound = (new Vector2(GameHandler.GraphicsHandler.Resolution.X, GameHandler.GraphicsHandler.Resolution.Y) - Size) / 2;
                Vector2 origin = Position - bound;
				Parent.DrawOrigin = new Vector2((int)origin.X, (int)origin.Y);
			}

            //if (InvinceTimer <= 0)
                HandleInput();

            if (Health > 0)
            {
                HandleCollision();
            }
        }

		public override void Draw() {
			base.Draw();
		}

		public void HandleInput() {
			if (Animation == CelebrateAnimation) {
				Velocity = Vector2.Zero;
			} else {
				float speedMultiplier = 1F;

				// Movements speed buffs/debuffs

				// Handle input

                //Attack move
				if (GameHandler.InputHandler.AnyKeyDown(AttackKey) && AttackTimer <= 0) { 
                    if (Velocity.X < 0)
                    {
                        Bullet bullet = new Bullet(false);
                        AddChild(bullet);
                    }
                    else if (Velocity.X > 0)
                    {
                        Bullet bullet = new Bullet(true);
                        AddChild(bullet);
                    }
                    Animation = AttackAnimation;
                    AttackTimer = 2;
				}
				if (GameHandler.InputHandler.AnyKeyDown(LeftKey) && Animation != CrouchAnimation) {
					Velocity.X = LeftSpeed * speedMultiplier;
				} else if (GameHandler.InputHandler.AnyKeyDown(RightKey) && Animation != CrouchAnimation) {
					Velocity.X = RightSpeed * speedMultiplier;
                } else if (_OnGround) {
					Velocity.X = 0F;
				}
				if (GameHandler.InputHandler.AnyKeyDown(JumpKey) && _OnGround && Animation != CrouchAnimation) {
					Jump(JumpSpeed);
                    Animation = JumpAnimation;
				}

			}
		}

		public void HandleCollision() {
			if (!_Won) {
				_OnGround = false;
                Trampo = false;


				Position = new Vector2((float)Math.Floor(Position.X), (float)Math.Floor(Position.Y));

				// Handle collisions
				foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
					// Win
					if (entity is GoalTile) {
                        _Won = true;
						if (_Won) {
							((PlayingState)Parent.Parent).Won = true;
							//Animation = CelebrateAnimation;
							break;
						}
					} else {
                        // Get launched?
                        if(entity is TeddyBear)
                        {
                            _OnGround = true;
                            if (chapter != 1 && chapter!=3 && TeddyBear.canAttack)
                            {
                                Damage();
                                KnockBack();
                                Jump(JumpSpeed);
                            }
                        }else 
                        
                        if(entity is Raindrop)
                        {
                            //Damage();
                            //Jump(JumpSpeed * 0.1F);
                            InvinceTimer = 0.3F;
                        }else

                        if(entity is BlockBoat)
                        {
                            //entity.Velocity.X = 200F;
                        }else
                        
                        if(entity is Board)
                        {
                            Damage();
                            InvinceTimer = 0.2F;
                        }
                        if (entity is CollisionObject)
                        {
                            _OnGround = true;
                        }
                        if(entity is TrainTracks)
                        {
                            //TODO: add soundeffect
                            Damage();
                            Velocity.X *= -1F;
                            Jump(JumpSpeed);
                            break;
                        }else if (entity is GuitarShotgun){
                            item = "shotgun";
                            
                        }else if (entity is TreeTile && chapter == 8){
                            item = "sword";
                            //TODO: sprite vd boom veradert naar normale boom
                        }else
                        if (entity is Train)
                        {
                            TrainSpeed = entity.Velocity;
                            _OnGround = true;
                            _OnRails = true;
                            Velocity.Y = 0F;
                            System.Diagnostics.Debug.Print(entity.Position.ToString());
                            if (Velocity.X == 0 && Animation != CrouchAnimation)
                            {
                                Animation = IdleAnimation;

                            }
                            Velocity.X += TrainSpeed.X;
                            System.Diagnostics.Debug.Print(entity.Velocity.ToString());
                        }
                        else if (!(entity is Train))
                            _OnRails = false;
                        else if (entity.Velocity.X < 100)
                            _OnRails = false;


                        if (!(entity is PlayerCreature) || (entity is Train) || (entity is TeddyBear) || !(entity is Raindrop)){
							RectangleF playerBounds = GlobalCollisionBox;
							RectangleF tileBounds = entity.GlobalCollisionBox;
							playerBounds.Height++;
							Vector2 depth = CalculateIntersectionDepth(playerBounds, tileBounds);

                            if (Math.Abs(depth.X) < Math.Abs(depth.Y)) {
								if (!(entity is PlatformTile)) {
									Position.X += depth.X;
								}
							} else {
								if (_PreviousY - 1 <= tileBounds.Top && Velocity.Y >= 0) {
									_OnGround = true;
                                    if (entity is TrampolineBedTile)
                                    {
                                        Trampo = true;
                                        Jump(JumpSpeed * 10F);
                                    }

									//if(Velocity.Y > 1500) {
										//Health -= (int)((Velocity.Y - 1500) / 50);
									//}
									Velocity.Y = 0F;
								}
								if (!(entity is PlatformTile) || (entity is PlatformTile && _OnGround)) {
									Position.Y += depth.Y + 1F;
									Velocity.Y = 0F;
                                }else if(entity is PlatformTile && !_OnGround && Velocity.Y > 0F)
                                {
                                    //Position.Y -= depth.Y - 1;
                                }
							}

						}
     
                    }
				}

				_PreviousY = GlobalCollisionBox.Bottom;
			}

		}

		public Vector2 CalculateIntersectionDepth(Rectangle rectangle1, Rectangle rectangle2) {
			Vector2 center1 = new Vector2(rectangle1.Center.X, rectangle1.Center.Y);
			Vector2 center2 = new Vector2(rectangle2.Center.X, rectangle2.Center.Y);
			Vector2 minDistance = new Vector2(rectangle1.Width + rectangle2.Width, rectangle1.Height + rectangle2.Height) / 2;
			Vector2 distance = center1 - center2;
			Vector2 depth = Vector2.Zero;
			if (distance.X > 0) {
				depth.X = minDistance.X - distance.X;
			} else {
				depth.X = -minDistance.X - distance.X;
			}
			if (distance.Y > 0) {
				depth.Y = minDistance.Y - distance.Y;
			} else {
				depth.Y = -minDistance.Y - distance.Y;
			}
			return depth;
		}

		public void Jump(float speed) {
			Velocity.Y = speed * 0.5F;
            if (Trampo)
                Velocity.Y = speed * 1.2F;
            //GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_jump"));
        }

		public void Explode() {
			if(Health > 0) {
				Health = 0;
				Velocity = Vector2.Zero;
				Animation = ExplodeAnimation;
			}
		}

        public void Damage()
        {
            if(InvinceTimer <= 0F)
                Health -= 2;
        }

        public void KnockBack()
        {
            Velocity.X = -100;
            Jump(JumpSpeed);
            Animation = IdleAnimation;
            KnockbackTimer = 1;
        }
        private void BikeLevel()
        {
            
            _Timer += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

            if (_Timer >= 60)
            {
                //next level
            }

            Animation = CyclingAnimation;

            Road road = new Road();
            Parent.AddChild(road);
            road.Position = Vector2.Zero;
            road.Size = new Vector2(GameHandler.GraphicsHandler.ScreenSize.X, GameHandler.GraphicsHandler.ScreenSize.Y);
            lane = 3;

            if (GameHandler.InputHandler.AnyKeyDown(JumpKey) && lane != 1)
            {
                lane -= 1;
            }
            if (GameHandler.InputHandler.AnyKeyDown(CrouchKey) && lane != 3)
            {
                lane += 1;
            }

            Position = new Vector2(100, GameHandler.GraphicsHandler.ScreenSize.Y / 4 + lane * GameHandler.GraphicsHandler.ScreenSize.Y / 4);

            if (GameHandler.Random.Next(50) == 0)
            {
                Obstacle obstacle = new Obstacle();
                obstacle.Position.Y = GameHandler.GraphicsHandler.ScreenSize.Y / 4 + GameHandler.Random.Next(1, 4) * GameHandler.GraphicsHandler.ScreenSize.Y / 4;
                obstacle.Position.X = GameHandler.GraphicsHandler.ScreenSize.X;
            }
        }

        private void CarLevel()
        {
            Animation = CarAnimation;

            

            if (GameHandler.InputHandler.AnyKeyDown(RightKey))
            {
                //this.Angle += 1;
            }
            else if (GameHandler.InputHandler.AnyKeyDown(LeftKey))
            {
                //this.Angle -= 1;
            }

            AsteroidSpawner spawner = new AsteroidSpawner();
            spawner.Position.X = GameHandler.GraphicsHandler.ScreenSize.X;
            spawner.Position.Y = 0;
            spawner.Update();
        }

	}
}
