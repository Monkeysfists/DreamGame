using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary {
	/// <summary>
	/// Handles graphics-related operations.
	/// </summary>
	public class GraphicsHandler {
		/// <summary>
		/// The SpriteBatch.
		/// </summary>
		public SpriteBatch SpriteBatch {
			get {
				return _SpriteBatch;
			}
		}

		private GraphicsDeviceManager _GraphicsDeviceManager;
		private SpriteBatch _SpriteBatch;

		/// <summary>
		/// Creates a new GraphicsHandler.
		/// </summary>
		/// <param name="graphicsDevice">The GraphicsDevice to use.</param>
		/// <param name="graphicsDeviceManager">The GraphicsDeviceManager to use.</param>
		public GraphicsHandler(GraphicsDeviceManager graphicsDeviceManager) {
			_GraphicsDeviceManager = graphicsDeviceManager;
		}

		/// <summary>
		/// Must be executed upon loading content.
		/// </summary>
		public void LoadContent() {
			_SpriteBatch = new SpriteBatch(_GraphicsDeviceManager.GraphicsDevice);
		}

		/// <summary>
		/// Clears the screen to a certain color.
		/// </summary>
		/// <param name="color"></param>
		public void Clear(Color color) {
			_GraphicsDeviceManager.GraphicsDevice.Clear(color);
		}

		/// <summary>
		/// Draws a texture on the screen.
		/// </summary>
		/// <param name="position">The position to draw the texture at.</param>
		/// <param name="size">The size of the texture.</param>
		/// <param name="texture">The texture to draw.</param>
		/// <param name="tint">The tint to apply to the texture.</param>
		public void DrawTexture(Vector2 position, Vector2 size, Texture2D texture, Color? tint = null) {
			_SpriteBatch.Draw(texture, new RectangleF(position.X, position.Y, size.X, size.Y), tint ?? Color.White);
		}

		/// <summary>
		/// Draws a rectangle on the screen.
		/// </summary>
		/// <param name="position">The position to draw the rectangle at.</param>
		/// <param name="size">The size of the rectangle.</param>
		/// <param name="color">The color of the rectangle.</param>
		public void DrawRectangle(Vector2 position, Vector2 size, Color color) {
			DrawTexture(position, size, GetColoredTexture(color));
		}

		/// <summary>
		/// Draws a box on the screen.
		/// </summary>
		/// <param name="position">The position to draw the box at.</param>
		/// <param name="size">The size of the box.</param>
		/// <param name="borderSize">The size of the border.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="borderColor">The color of the border of the box.</param>
		public void DrawSquare(Vector2 position, Vector2 size, float borderSize, Color color, Color borderColor) {
			// Top line
			DrawRectangle(position, new Vector2(size.X, borderSize), borderColor);

			// Bottom line
			DrawRectangle(new Vector2(position.X, position.Y + size.Y - borderSize), new Vector2(size.X, borderSize), borderColor);

			// Left line
			DrawRectangle(new Vector2(position.X, position.Y + borderSize), new Vector2(borderSize, size.Y - 2 * borderSize), borderColor);

			// Right line
			DrawRectangle(new Vector2(position.X + size.X - borderSize, position.Y + borderSize), new Vector2(borderSize, size.Y - 2 * borderSize), borderColor);

			// Center
			DrawRectangle(new Vector2(position.X + borderSize, position.Y + borderSize), new Vector2(size.X - 2 * borderSize, size.Y - 2* borderSize), color);
		}

		/// <summary>
		/// Gets a 1 by 1 texture of the given color.
		/// </summary>
		/// <param name="color">The color.</param>
		/// <returns>A 1 by 1 texture.</returns>
		public Texture2D GetColoredTexture(Color color) {
			Texture2D texture = new Texture2D(_GraphicsDeviceManager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

			// Set color
			texture.SetData<Color>(new Color[] {
				color
			});

			return texture;
		}
	}
}
