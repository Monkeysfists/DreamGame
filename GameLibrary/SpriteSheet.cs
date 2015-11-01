using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameLibrary {
	/// <summary>
	/// A spritesheet that contains many textures.
	/// </summary>
	public class SpriteSheet {
		/// <summary>
		/// The SpriteSheet texture.
		/// </summary>
		public Texture2D Texture;
		/// <summary>
		/// The dimensions of the SpriteSheet.
		/// </summary>
		public Point Dimensions;
		/// <summary>
		/// The size of an individual cell in the SpriteSheet.
		/// </summary>
		public Vector2 CellSize {
			get {
				return new Vector2(Texture.Width / Dimensions.X, Texture.Height / Dimensions.Y);
			}
		}

		/// <summary>
		/// Creates a new SpriteSheet from the specified texture.
		/// </summary>
		/// <param name="texture"></param>
		public SpriteSheet(string texture) {
			Dimensions = Point.Zero;

			Texture = GameHandler.AssetHandler.GetTexture(texture);

			// Get the spritesheet size
			String[] splitAsset = texture.Split('@');
			if(splitAsset.Length > 1) {
				String[] dimensions = splitAsset[splitAsset.Length - 1].Split('x');
				if(dimensions.Length > 1) {
					Dimensions = new Point(int.Parse(dimensions[0]), int.Parse(dimensions[1]));
				} else {
					Dimensions = new Point(int.Parse(splitAsset[splitAsset.Length - 1]), 1);
                }
			} else {
				// No dimensions, single sprite
				Dimensions = new Point(1, 1);
			}
		}
	}
}
