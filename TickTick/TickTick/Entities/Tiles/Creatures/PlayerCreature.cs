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
				if (value < Health) {
					if (Position.Y > Parent.Size.Y) {
						//GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_fall"));
					} else {
						//GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_die"));
					}
				}

				_Health = value;

				if (Health <= 0) {
					if (Position.Y <= Parent.Size.Y) {
						Velocity.Y = -900;
					}

					Animation = DieAnimation;
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
        /// <summary>
        /// Invincible timer control bool
        /// </summary>
        private bool Invin;
        private bool _OnRails;

		private int _Health;
		private bool _OnGround;
		private float _PreviousY;
		private bool _Won;
        private int TemporaryHealth;
        private float AttackTimer;
        private float InvinceTimer;
        public string item;
        private int chapter;
        public float trainTimer;
        public bool Trampo;

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

			// Animations
			IdleAnimation = new PlayerIdleAnimation(chapter, item);
			LeftAnimation = new PlayerMoveAnimation(chapter, item);
			LeftAnimation.FlipHorizontally = true;
			RightAnimation = new PlayerMoveAnimation(chapter, item);
			JumpAnimation = new PlayerJumpAnimation(chapter, item);
			DieAnimation = new PlayerDieAnimation(chapter);
            CrouchAnimation = new PlayerCrouchAnimation(chapter);
            AttackAnimation = new PlayerAttackAnimation(chapter, item);
			Animation = IdleAnimation;

			// Size
			Size = Animation.SpriteSheet.CellSize;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2;

			// Speed
			LeftSpeed = -200F;
			RightSpeed = 200F;
			JumpSpeed = -1100F;

            //Timer
            InvinceTimer = 0;
            trainTimer = 3;

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
		}

		public override void Update() {
            if (Health > 0)
            {
                HandleCollision();
            }

            if (Health > 0 ) {
                if (_OnRails) ;
                    //Velocity.X = RightSpeed;
                
                if(InvinceTimer <= 0)
                    HandleInput();

                //Update timer if needed
                if (InvinceTimer > 0F)
                {
                    InvinceTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                    Velocity = Vector2.Zero;
                } else if (AttackTimer > 0) {
                    AttackTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                } else if (trainTimer > 0)
                    trainTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;


                if (Animation != CelebrateAnimation && InvinceTimer <= 0) {
					// Animation directions
					if (Velocity.X > 0) {
						IdleAnimation.FlipHorizontally = false;
						JumpAnimation.FlipHorizontally = false;
                        CrouchAnimation.FlipHorizontally = false;
					} else if (Velocity.X < 0) {
						IdleAnimation.FlipHorizontally = true;
						JumpAnimation.FlipHorizontally = true;
                        CrouchAnimation.FlipHorizontally = true;
					}

					// Set the correct animation
					if (_OnGround) {
						if (Velocity.X == 0) {
							// On the ground and not moving
							Animation = IdleAnimation;
						} else {
							// On the ground, but moving
							if (Velocity.X > 0) {
								Animation = RightAnimation;
							} else if (Velocity.X < 0) {
								Animation = LeftAnimation;
							}
						}
					} else if (Velocity.Y != 0) {
						Animation = JumpAnimation;
					}
                    if (Velocity.X == 0 && Animation != CrouchAnimation)
                        Animation = IdleAnimation;
				}

				PlayingState state = ((PlayingState)Parent.Parent);
					//state.Timer.Multiplier = 1;

				// Kill if offscreen
				if (Position.Y > Parent.Size.Y) {
					Health = 0;
				}


			}

            //Velocity.Y = 0F;

            if (GameHandler.InputHandler.AnyKeyDown(CrouchKey))
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
                    //TODO attack
				}
				if (GameHandler.InputHandler.AnyKeyDown(LeftKey) && Animation != CrouchAnimation) {
					Velocity.X = LeftSpeed * speedMultiplier;
				} else if ((GameHandler.InputHandler.AnyKeyDown(RightKey) || ((_OnRails ) && trainTimer <= 0)) && Animation != CrouchAnimation) {
					Velocity.X = RightSpeed * speedMultiplier;
                    if (_OnRails && GameHandler.InputHandler.AnyKeyDown(RightKey))
                        Velocity.X = RightSpeed * speedMultiplier * 2;
                } else if (_OnGround) {
					Velocity.X = 0F;
				}
				if (GameHandler.InputHandler.AnyKeyDown(JumpKey) && _OnGround && Animation != CrouchAnimation) {
					Jump(JumpSpeed);
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
							Animation = CelebrateAnimation;
							break;
						}
					} else {
                        // Get launched?
                        if(entity is TrampolineBedTile)
                        {
                            _OnGround = true;
                        }
                        if(entity is TeddyBear)
                        {
                            _OnGround = true;
                            if(chapter != 1)
                                Damage();
                            InvinceTimer = 1F;
                            if (chapter == 1)
                                InvinceTimer = 10F;
                            KnockBack();
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
                            //TODO: sprite van guitarshotgun wordt "Emptystand"
                        }else if (entity is TreeTile && chapter == 3){
                            item = "sword";
                            //TODO: sprite vd boom veradert naar normale boom
                        }
                       
                        
                        if(!(entity is PlayerCreature) || (entity is Train) || (entity is TeddyBear)){
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
								if (!(entity is PlatformTile) || (entity is PlatformTile && _OnGround) || entity is TrampolineBedTile) {
									Position.Y += depth.Y + 1;
									Velocity.Y = 0F;
                                }else if(entity is PlatformTile && !_OnGround && Velocity.Y > 0F)
                                {
                                    //Position.Y -= depth.Y - 1;
                                }
							}
						}
                        if (entity is Train)
                        {
                            _OnRails = true;
                            _OnGround = true;
                            Velocity.Y = 0F;
                            if (Velocity.X == 0 && Animation != CrouchAnimation)
                            {
                                Animation = IdleAnimation;

                            }
                        }
                        else if (!(entity is Train))
                            _OnRails = false;
                        else if (entity.Velocity.X < 100)
                            _OnRails = false;
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
			Velocity.Y = speed * 0.7F;
            if (Trampo)
                Velocity.Y = speed * 0.9F;
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
            Velocity.Y = JumpSpeed * 0.5F;
            Velocity.X = -100;
            Animation = IdleAnimation;
            
        }
	}
}
