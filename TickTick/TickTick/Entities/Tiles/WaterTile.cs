using GameLibrary;
using Microsoft.Xna.Framework;
using System;

namespace TickTick.Entities.Tiles {
	/// <summary>
	/// A water droplet, which can be picked up.
	/// </summary>
	public class WaterTile : TileEntity {
		/// <summary>
		/// Creates a new WaterTile.
		/// </summary>
		public WaterTile() {
			Texture = GameHandler.AssetHandler.GetTexture("Sprites/spr_water");
			Origin = new Vector2(-((Size.X - Texture.Width) / 2), 10);
			ResizeToTexture();
		}

		public override void Update() {
			base.Update();

			// Make the water drop bounce
			double x = GameHandler.GameTime.TotalGameTime.TotalSeconds * 3.0F + Position.X;
			Position.Y += (float)Math.Sin(x) * 0.2f;
		}
	}
}
