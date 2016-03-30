using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Creatures;
using TickTick.Entities.Tiles.Platforms;

namespace TickTick.Entities.Tiles.Walls {
	/// <summary>
	/// A wall tile, which can be walked on.
	/// </summary>
	public class WallTile : TileEntity {
		private Vector2 _LastPosition;

		public WallTile() {
			CanCollide = true;
		}

		public override void Update() {
			if (Velocity != Vector2.Zero) {
				foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero)) {
					if (entity is WallTile || entity is PlatformTile) {
						Velocity *= -1;
					}
					if(entity is playerCreature) {
                        entity.Position += Position - _LastPosition;
					}
				}
			}

			_LastPosition = Position;

			base.Update();
		}
	}
}
