using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Creatures;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Platforms {
	/// <summary>
	/// A platform tile, which can be walked on and jumped through from below.
	/// </summary>
	public class PlatformTile : TileEntity {
		private Vector2 _LastPosition;

		public PlatformTile() {
			CanCollide = true;
		}

		public override void Update() {
			if (Velocity != Vector2.Zero) {
				foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
					if (entity is WallTile || entity is PlatformTile) {
						Velocity *= -1;
					}
					if (entity is PlayerCreature) {
						entity.Position += Position - _LastPosition;
					}
				}
			}

			_LastPosition = Position;

			base.Update();
		}
	}
}
