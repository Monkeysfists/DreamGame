using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Animations;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// A rocket creature.
	/// </summary>
	public class RocketCreature : CreatureTileEntity {
		/// <summary>
		/// The animation to play when moving.
		/// </summary>
		public Animation MoveAnimation;
		/// <summary>
		/// The speed to move at.
		/// </summary>
		public Vector2 MoveSpeed;

		private bool _StartPositionSet;
		private double _SpawnTime;
		private Vector2 _StartPosition;

		/// <summary>
		/// Creates a new RocketCreature.
		/// </summary>
		public RocketCreature() {
			// Animations
			IdleAnimation = new RocketMoveAnimation();
			MoveAnimation = new RocketMoveAnimation();
			Animation = IdleAnimation;

			// Size
			Size = Animation.SpriteSheet.CellSize;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2;
			CollisionBox = new RectangleF(Origin.X, Origin.Y, 72, 55);

			_StartPosition = Vector2.Zero;
			_StartPositionSet = false;
			MoveSpeed = Vector2.Zero;

			Reset();
		}

		public override void Update() {
			// Set initial position
			if (!_StartPositionSet) {
				if(MoveSpeed.X > 0) {
					Position.X = 0 - CollisionBox.Width;
				} else {
					Position.X = Parent.Size.X + CollisionBox.Width;
				}
				_StartPosition = Position;
				_StartPositionSet = true;
			}

			if (_SpawnTime > 0) {
				_SpawnTime -= GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
			} else {
				Visible = true;
				Velocity = MoveSpeed;
			}

			if (Velocity == Vector2.Zero) {
				Animation = IdleAnimation;
			} else {
				Animation = MoveAnimation;
			}

			// Animation directions
			if (Velocity.X > 0) {
				IdleAnimation.FlipHorizontally = false;
				MoveAnimation.FlipHorizontally = false;
			} else if (Velocity.X < 0) {
				IdleAnimation.FlipHorizontally = true;
				MoveAnimation.FlipHorizontally = true;
			}

			// Check if off screen
			if (!Parent.GlobalBoundingBox.Intersects(GlobalBoundingBox)) {
				Reset();
			}

			HandleCollision();

			base.Update();
		}

		public void Reset() {
			Visible = false;
			if (_StartPositionSet) {
				Position = _StartPosition;
			}
			Velocity = Vector2.Zero;
			_SpawnTime = GameHandler.Random.NextDouble() * 5;
		}

		public void HandleCollision() {
			if(Visible) {
				// Handle collisions
				foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
					if (entity is PlayerCreature) {
						PlayerCreature player = (PlayerCreature)entity;

						if (player.Health > 0) {
							if(player.Velocity.Y > 0 && player.GlobalBoundingBox.Bottom < GlobalBoundingBox.Bottom) {
								player.Jump(-1000);
							} else {
								player.Health -= 50;
							}

							Parent.RemoveChild(this);
						}
					}
				}
			}
		}
	}
}
