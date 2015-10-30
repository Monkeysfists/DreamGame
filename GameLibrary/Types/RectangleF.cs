using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Types {
	public class RectangleF {
		/// <summary>
		/// The X coordinate.
		/// </summary>
		public float X;
		/// <summary>
		/// The Y coordinate.
		/// </summary>
		public float Y;
		/// <summary>
		/// The width.
		/// </summary>
		public float Width;
		/// <summary>
		/// The height.
		/// </summary>
		public float Height;
		/// <summary>
		/// The X coordinate of the left side.
		/// </summary>
		public float Left {
			get {
				return X;
			}
		}
		/// <summary>
		/// The X coordinate of the right side.
		/// </summary>
		public float Right {
			get {
				return X + Width;
			}
		}
		/// <summary>
		/// The Y coordinate of the top.
		/// </summary>
		public float Top {
			get {
				return Y;
			}
		}
		/// <summary>
		/// The Y coordinate of the bottom.
		/// </summary>
		public float Bottom {
			get {
				return Y + Height;
			}
		}

		/// <summary>
		/// Creates a new RectangleF.
		/// </summary>
		/// <param name="x">The X coordinate.</param>
		/// <param name="y">The Y coordinate.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public RectangleF(float x, float y, float width, float height) {
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public static implicit operator Rectangle(RectangleF rectangleF) {
			return new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
		}

		/// <summary>
		/// Gets whether this RectangleF contains the specified vector.
		/// </summary>
		/// <param name="vector">The vector to check.</param>
		/// <returns>Whether the rectangle contains the vector.</returns>
		public bool Contains(Vector2 vector) {
			return vector.X >= X && vector.Y >= Y && vector.X <= X + Width && vector.Y <= Y + Height;
		}

		/// <summary>
		/// Gets whether this RectangleF intersects with another RectangleF.
		/// </summary>
		/// <param name="rectangle">The rectangle to check.</param>
		/// <returns>Whether the rectangles intersect.</returns>
		public bool Intersects(RectangleF rectangle) {
			return !(rectangle.Left > Right ||
					rectangle.Right < Left ||
					rectangle.Top > Bottom ||
					rectangle.Bottom < Top);
		}
	}
}
