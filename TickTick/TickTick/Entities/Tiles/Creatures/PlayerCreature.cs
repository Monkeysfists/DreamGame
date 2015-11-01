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
						GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_fall"));
					} else {
						GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_die"));
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

		private int _Health;
		private bool _OnGround;
		private bool _OnIce;
		private bool _OnHot;
		private float _PreviousY;
		private bool _Won;
        private bool _Banana;
        private float SlowTimer;
        private float ShieldTimer;
        private int TemporaryHealth;
        private float BombTimer;

		/// <summary>
		/// Creates a new PlayerCreature.
		/// </summary>
		public PlayerCreature() {
			Health = 100;
			_PreviousY = GlobalCollisionBox.Bottom;
			Name = "player";
			_Won = false;

			// Animations
			IdleAnimation = new PlayerIdleAnimation();
			LeftAnimation = new PlayerMoveAnimation();
			LeftAnimation.FlipHorizontally = true;
			RightAnimation = new PlayerMoveAnimation();
			JumpAnimation = new PlayerJumpAnimation();
			CelebrateAnimation = new PlayerCelebrateAnimation();
			DieAnimation = new PlayerDieAnimation();
			ExplodeAnimation = new PlayerExplodeAnimation();
			Animation = IdleAnimation;

			// Size
			Size = Animation.SpriteSheet.CellSize;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2;

			// Speed
			LeftSpeed = -400F;
			RightSpeed = 400F;
			JumpSpeed = -1100F;

            //Timer
            SlowTimer = 0;

			// Input
			LeftKey = new List<Keys>();
			RightKey = new List<Keys>();
			JumpKey = new List<Keys>();
			LeftKey.Add(Keys.A);
			LeftKey.Add(Keys.Left);
			RightKey.Add(Keys.D);
			RightKey.Add(Keys.Right);
			JumpKey.Add(Keys.W);
			JumpKey.Add(Keys.Up);
			JumpKey.Add(Keys.Space);
		}

		public override void Update() {
			if (Health > 0) {
				HandleInput();

                //Update timer if needed
                if(SlowTimer > 0)
                {
                    SlowTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                }else if(SlowTimer <= 0)
                {
                    _Banana = false;
                }
                if (ShieldTimer > 0)
                {
                    ShieldTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                    Health = TemporaryHealth;
                }
                if(BombTimer > 0)
                    BombTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;


                if (Animation != CelebrateAnimation) {
					// Animation directions
					if (Velocity.X > 0) {
						IdleAnimation.FlipHorizontally = false;
						JumpAnimation.FlipHorizontally = false;
					} else if (Velocity.X < 0) {
						IdleAnimation.FlipHorizontally = true;
						JumpAnimation.FlipHorizontally = true;
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
				}

				PlayingState state = ((PlayingState)Parent.Parent);
				if(_OnHot) {
					state.Timer.Multiplier = 2;
				} else if(_OnIce) {
					state.Timer.Multiplier = 0.5;
				} else {
					state.Timer.Multiplier = 1;
				}

				// Kill if offscreen
				if (Position.Y > Parent.Size.Y) {
					Health = 0;
				}
			}

			base.Update();

			// Handle physics
			if (Animation != ExplodeAnimation) {
				Velocity.Y += 55F; // Fall speed
			}

			if (Health > 0) {
				HandleCollision();
			}

			if (Health > 0) {
				Vector2 bound = (new Vector2(GameHandler.GraphicsHandler.Resolution.X, GameHandler.GraphicsHandler.Resolution.Y) - Size) / 2;
                Vector2 origin = Position - bound;
				Parent.DrawOrigin = new Vector2((int)origin.X, (int)origin.Y);
			}
		}

		public override void Draw() {
			// Draw health
			if (Health < 100 && Health > 0) {
				float green = (float)Health / 100 * Size.X;
				float red = Size.X - green;

				GameHandler.GraphicsHandler.DrawRectangle(new Vector2(GlobalPosition.X - Origin.X - GlobalDrawOrigin.X + green, GlobalPosition.Y - Origin.Y - GlobalDrawOrigin.Y - 20F), new Vector2(red, 10F), Color.Red);
				GameHandler.GraphicsHandler.DrawRectangle(new Vector2(GlobalPosition.X - Origin.X - GlobalDrawOrigin.X, GlobalPosition.Y - Origin.Y - GlobalDrawOrigin.Y - 20F), new Vector2(green, 10F), Color.Green);
			}

			base.Draw();
		}

		public void HandleInput() {
			if (Animation == CelebrateAnimation) {
				Velocity = Vector2.Zero;
			} else {
				float speedMultiplier = 1F;

				// Ice multiplier
				if (_OnIce) {
					speedMultiplier = 1.5F;
				}

                if (_Banana)
                    speedMultiplier = 0.5F;

				// Handle input
				if (GameHandler.InputHandler.OnKeyDown(Keys.Enter) && BombTimer <= 0) {
					BombEntity bomb = new BombEntity();
                    BombTimer = 2;
					bomb.Position = Position;
					bomb.Velocity = Velocity;
					bomb.Velocity.Y -= 1000F;
					if(IdleAnimation.FlipHorizontally) {
						bomb.Velocity.X = Velocity.X - 500F;
					} else {
						bomb.Velocity.X = Velocity.X + 500F;
					}
					bomb.Layer = 10;
					Parent.AddChild(bomb);
				}
				if (GameHandler.InputHandler.AnyKeyDown(LeftKey)) {
					Velocity.X = LeftSpeed * speedMultiplier;
				} else if (GameHandler.InputHandler.AnyKeyDown(RightKey)) {
					Velocity.X = RightSpeed * speedMultiplier;
				} else if (!_OnIce && _OnGround) {
					Velocity.X = 0F;
				}
				if (GameHandler.InputHandler.AnyKeyDown(JumpKey) && _OnGround) {
					Jump(JumpSpeed);
				}
			}
		}

		public void HandleCollision() {
			if (!_Won) {
				_OnGround = false;
				_OnIce = false;
				_OnHot = false;

				Position = new Vector2((float)Math.Floor(Position.X), (float)Math.Floor(Position.Y));

				// Handle collisions
				foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
					// Win
					if (entity is GoalTile) {
						_Won = true;
						foreach (Entity checkEntity in Parent.Children) {
							if (checkEntity is WaterTile) {
								_Won = false;
							}
						}
						if (_Won) {
							((PlayingState)Parent.Parent).Won = true;
							Animation = CelebrateAnimation;
							break;
						}
					} else {
                        // Woops you just slipped on a banana
                        if(entity is BananaTile)
                        {
                            Parent.RemoveChild(entity);
                            _Banana = true;
                            SlowTimer = 3;
                        }
                        //WOOOHOOOO INVICIBLE
                        if(entity is ShieldTile)
                        {
                            Parent.RemoveChild(entity);
                            ShieldTimer = 7;
                            TemporaryHealth = Health;
                        }
						// Collect water
						if (entity is WaterTile) {
							Parent.RemoveChild(entity);
							GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected"));

							if (Health <= 99) {
								Health += 1;
							}
						} else if(!(entity is BombEntity)&& !(entity is CreatureTileEntity)){
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
									if(Velocity.Y > 1500) {
										Health -= (int)((Velocity.Y - 1500) / 10);
									}
									Velocity.Y = 0F;

									_OnIce = entity is IcePlatform || entity is IceWall;
									_OnHot = entity is HotPlatform || entity is HotWall;
								}
								if (!(entity is PlatformTile) || (entity is PlatformTile && _OnGround)) {
									Position.Y += depth.Y + 1;
									Velocity.Y = 0F;
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
			Velocity.Y = speed;
			GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_jump"));
		}

		public void Explode() {
			if(Health > 0) {
				Health = 0;
				Velocity = Vector2.Zero;
				Animation = ExplodeAnimation;
			}
		}
	}
}
