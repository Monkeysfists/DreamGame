using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// A sparky figure.
	/// </summary>
	public class SparkyCreature : CreatureTileEntity {
		/// <summary>
		/// The animation to play when electrocuting.
		/// </summary>
		public Animation ElectrocuteAnimation;

		private float _IdleTime;
		private float _YOffset;
		private float _InitialY;
		private bool _InitialYSet;
		private float _HurtWaitTime;

		/// <summary>
		/// Creates a new SparkyCreature.
		/// </summary>
		public SparkyCreature() {
			// Animations
			IdleAnimation = new SparkyIdleAnimation();
			ElectrocuteAnimation = new SparkyElectrocuteAnimation();
			Animation = IdleAnimation;

			// Size
			Size = Animation.SpriteSheet.CellSize;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2;
			CollisionBox = new RectangleF(Origin.X, Origin.Y, 72, 55);

			_InitialY = 0F;
			_InitialYSet = false;
			_HurtWaitTime = 0F;

			Reset();
		}

		public override void Update() {
			_HurtWaitTime -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

			// Set initial position
			if (!_InitialYSet) {
				Position.Y -= 175F;
				_InitialY = Position.Y;
				_InitialYSet = true;
			}

			if (_IdleTime <= 0) {
				Animation = ElectrocuteAnimation;
				if (Velocity.Y != 0) {
					// Falling down
					_YOffset -= Velocity.Y * (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
					if (_YOffset <= 0) {
						Velocity.Y = 0F;
					} else if (_YOffset >= 120) {
						Reset();
					}
				} else if (Animation.Cell.X == Animation.SpriteSheet.Dimensions.X - 1 && Animation.Cell.Y == Animation.SpriteSheet.Dimensions.Y - 1) {
					// Animation done
					Velocity.Y = -60F;
				}
			} else {
				Animation = IdleAnimation;
				_IdleTime -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
				if (_IdleTime <= 0F) {
					Velocity.Y = 300F;
				}
			}

			// Check if off screen
			if (!Parent.GlobalBoundingBox.Intersects(GlobalBoundingBox)) {
				Reset();
			}

			HandleCollision();

			base.Update();
		}

		public void Reset() {
			_IdleTime = (float)GameHandler.Random.NextDouble() * 5;
			if (_InitialYSet) {
				Position.Y = _InitialY;
			}
			_YOffset = 120F;
			Velocity = Vector2.Zero;
		}

		public void HandleCollision() {
			// Handle collisions
			if (Velocity.Y == 0 && _HurtWaitTime <= 0F && Animation == ElectrocuteAnimation && !(Animation.Cell.X == Animation.SpriteSheet.Dimensions.X - 1 && Animation.Cell.Y == Animation.SpriteSheet.Dimensions.Y - 1)) {
				foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
					// Collect water
					if (entity is PlayerCreature) {
						PlayerCreature player = (PlayerCreature)entity;

						if (player.Health > 0) {
							player.Health -= 25;
							_HurtWaitTime = 0.5F;
						}
					}
				}
			}
		}
	}
}
