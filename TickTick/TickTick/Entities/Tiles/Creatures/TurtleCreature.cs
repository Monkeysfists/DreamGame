using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// A turtle creature.
	/// </summary>
	public class TurtleCreature : CreatureTileEntity {
		/// <summary>
		/// The animation to play when sneezing.
		/// </summary>
		public Animation SneezeAnimation;

		private float _SneezeTime;
		private float _IdleTime;

		/// <summary>
		/// Creates a new TurtleCreature.
		/// </summary>
		public TurtleCreature() {
			// Animations
			IdleAnimation = new TurtleIdleAnimation();
			SneezeAnimation = new TurtleSneezeAnimation();
			Animation = IdleAnimation;

			// Size
			Size = Animation.SpriteSheet.CellSize;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2 + 20;
			CollisionBox = new RectangleF(Origin.X, Origin.Y, 72, 55);

			// Start
			_SneezeTime = 0.0f;
			_IdleTime = 5.0f;
		}

		public override void Update() {
			// Update sneezes
			if (_SneezeTime > 0) {
				Animation = SneezeAnimation;
				_SneezeTime -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
				if (_SneezeTime <= 0.0f) {
					_IdleTime = 5.0f;
					_SneezeTime = 0.0f;
				}
			} else if (_IdleTime > 0) {
				Animation = IdleAnimation;
				_IdleTime -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
				if (_IdleTime <= 0.0f) {
					_IdleTime = 0.0f;
					_SneezeTime = 5.0f;
				}
			}

			HandleCollision();

			base.Update();
		}

		public void HandleCollision() {
			// Handle collisions
			foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
				// Collect water
				if (entity is PlayerCreature) {
					PlayerCreature player = (PlayerCreature)entity;

					if(player.Health > 0) {
						if (_SneezeTime > 0F) {
							player.Health -= 100;
						}

						if (_IdleTime > 0F && player.Velocity.Y > 0F) {
							player.Jump(-1500);
						}
					}
				}
			}
		}
	}
}
