using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GameLibrary {
	public class SpriteSheet {
		/// <summary>
		/// The SpriteSheet texture.
		/// </summary>
		public Texture2D Texture;
		/// <summary>
		/// A grid containing the textures in the SpriteSheet.
		/// </summary>
		public Texture2D [,] Textures {
			get {
				Texture2D[,] result = new Texture2D[Dimensions.X, Dimensions.Y];

				for(int column = 0; column < Dimensions.X; column++) {
					for(int row = 0; row < Dimensions.Y; row++) {
						RectangleF rectangle = new RectangleF(column * CellSize.X, row * CellSize.Y, CellSize.X, CellSize.Y);

						result[column, row] = GetSubtexture(rectangle);
					}
				}

				return result;
			}
		}
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
		/// Creates a new SpriteSheet from the specified asset.
		/// </summary>
		/// <param name="asset"></param>
		public SpriteSheet(string asset) {
			Dimensions = Point.Zero;

			Texture = GameHandler.AssetHandler.GetTexture(asset);

			// Get the spritesheet size
			string[] splitAsset = asset.Split('@');
			if(splitAsset.Length > 1) {
				string[] dimensions = splitAsset[0].Split('x');
				if(dimensions.Length > 1) {
					Dimensions.X = int.Parse(dimensions[0]);
					Dimensions.Y = int.Parse(dimensions[1]);
				}
			}
		}

		/// <summary>
		/// Gets a section of the texture.
		/// </summary>
		/// <param name="section">The section to get.</param>
		/// <returns>A section of the texture.</returns>
		private Texture2D GetSubtexture(RectangleF section) {
			Color[] allColor = new Color[Texture.Width * Texture.Height];
			Texture.GetData<Color>(allColor);
			Color[] color = new Color[(int)(section.Width * section.Height)];

			for(float x = 0; x < section.Width; x++) {
				for(float y = 0; y < section.Height; y++) {
					color[(int)(x + y * section.Width)] = allColor[(int)(x + section.X + (y + section.Y) * Texture.Width)];
				}
			}

			Texture2D result = GameHandler.GraphicsHandler.GetColoredTexture(Color.Transparent, new Point((int)section.X, (int)section.Y));
			result.SetData<Color>(color);

			return result;
		}
	}
}
