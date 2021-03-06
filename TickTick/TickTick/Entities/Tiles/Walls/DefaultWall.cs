﻿using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles.Walls {
	/// <summary>
	/// A default wall.
	/// </summary>
	public class DefaultWall : WallTile {
		/// <summary>
		/// Creates a new DefaultWall.
		/// </summary>
		public DefaultWall() {
			Texture = GameHandler.AssetHandler.GetTexture("stoep");
            Size = new Vector2(LevelEntity.TileSize, LevelEntity.TileSize);
		}
	}
}
