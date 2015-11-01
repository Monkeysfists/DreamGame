using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// An unpredictable flame creature.
	/// </summary>
	public class FlameCreature : CreatureTileEntity {
		/// <summary>
		/// The animation to play when moving.
		/// </summary>
		public Animation MoveAnimation;

		protected float _WaitTime;
		private float _HurtWaitTime;

		/// <summary>
		/// Creates a new FlameCreature.
		/// </summary>
		public FlameCreature() {
			// Animations
			IdleAnimation = new FlameMoveAnimation();
			MoveAnimation = new FlameMoveAnimation();
			Animation = IdleAnimation;

			// Size
			Size = Animation.SpriteSheet.CellSize;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2 + 30F;
			CollisionBox = new RectangleF(Origin.X, Origin.Y, 72, 55);

			_WaitTime = 0F;
			_HurtWaitTime = 0F;
			Velocity.X = 120F;
		}

		public override void Update() {
			_HurtWaitTime -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;

			if (_WaitTime > 0) {
				_WaitTime -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
				if (_WaitTime <= 0.0f) {
					TurnAround();
				}
			} else {
				float checkX = GlobalCollisionBox.Left - 1F;
				if (Velocity.X > 0) {
					checkX = GlobalCollisionBox.Right + 1F;
				}

				List<Entity> check = GetEntitiesAtPosition(new List<Entity>(Parent.Children), new Vector2(checkX, GlobalCollisionBox.Bottom + 1F));
				bool ground = false;
				foreach (Entity entity in check) {
					if (entity is PlatformTile || entity is WallTile) {
						ground = true;
						break;
					}
				}
				if (!ground) {
					_WaitTime = 0.5F;
					Velocity.X = 0F;
				}
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

			HandleCollision();

			base.Update();
		}

		public void TurnAround() {
			Velocity.X = 120;
			if (!IdleAnimation.FlipHorizontally) {
				Velocity.X = -Velocity.X;
			}
		}

		public void HandleCollision() {
			// Handle collisions
			if (_HurtWaitTime <= 0F) {
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
