using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.Tiles.Creatures;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities {
	/// <summary>
	/// A throwable bomb.
	/// </summary>
	public class BombEntity : AnimatedEntity {
		/// <summary>
		/// Creates a new BombEntity.
		/// </summary>
		/// <param name="start"></param>
		public BombEntity() {
			CanCollide = true;
			Animation = new PlayerIdleAnimation();
			Size = Animation.SpriteSheet.CellSize;
            Size = Size * 0.5F;
			Origin.X = (Size.X - 72) / 2;
			Origin.Y = (Size.Y - 55) / 2;
			CollisionBox = new RectangleF(Origin.X, Origin.Y, 72, 55);
		}

		public override void Update() {
			if (Animation is PlayerExplodeAnimation) {
				Velocity = Vector2.Zero;
				if (Animation.Cell.X == Animation.SpriteSheet.Dimensions.X - 1 && Animation.Cell.Y == Animation.SpriteSheet.Dimensions.Y - 1) {
					Parent.RemoveChild(this);
				}
			} else {
				Velocity.Y += 55F;

				foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
					if (entity is CreatureTileEntity && !(entity is PlayerCreature)) {
						Animation = new PlayerExplodeAnimation();
						Parent.RemoveChild(entity);
						break;
					} else if (entity is PlatformTile || entity is WallTile) {
						Animation = new PlayerExplodeAnimation();
						break;
					}
				}
			}

			if(this.Parent != null) {
				if (!Parent.GlobalBoundingBox.Intersects(GlobalBoundingBox)) {
					Parent.RemoveChild(this);
				}
			}

			base.Update();
		}
	}
}
