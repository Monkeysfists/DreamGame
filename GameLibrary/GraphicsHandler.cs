using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameLibrary {
	/// <summary>
	/// Handles graphics-related operations.
	/// </summary>
	public class GraphicsHandler {
		/// <summary>
		/// Switch the game between windowed and fullscreen.
		/// </summary>
		public bool FullScreen {
			get {
				return _GraphicsDeviceManager.IsFullScreen;
			}
			set {
				Vector2 scale = new Vector2((float)ScreenSize.X / (float)Resolution.X, (float)ScreenSize.Y / (float)Resolution.Y);
				float finalScale = 1F;

				if (value) {
					finalScale = scale.X;
					if (Math.Abs(1 - scale.Y) < Math.Abs(1 - scale.X)) {
						finalScale = scale.Y;
					}
				} else {
					if (scale.X < 1f || scale.Y < 1f) {
						finalScale = Math.Min(scale.X, scale.Y);
					}
				}

				_GraphicsDeviceManager.PreferredBackBufferWidth = (int)(finalScale * Resolution.X);
				_GraphicsDeviceManager.PreferredBackBufferHeight = (int)(finalScale * Resolution.Y);
				_GraphicsDeviceManager.IsFullScreen = value;
				_GraphicsDeviceManager.ApplyChanges();
				Scale = new Vector2((float)Viewport.Width / (float)Resolution.X, (float)Viewport.Height / (float)Resolution.Y);
			}
		}
		/// <summary>
		/// The actual size of the screen in pixels.
		/// </summary>
		public Point ScreenSize {
			get {
				return new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			}
		}
		/// <summary>
		/// The resolution at which to draw the game.
		/// </summary>
		public Point Resolution {
			get {
				return _Resolution;
			}
			set {
				_Resolution = value;

				// Refresh fullscreen
				FullScreen = FullScreen;
			}
		}
		/// <summary>
		/// The current visible portion of the game.
		/// </summary>
		public Rectangle Viewport {
			get {
				return new Rectangle(_GraphicsDeviceManager.GraphicsDevice.Viewport.X, _GraphicsDeviceManager.GraphicsDevice.Viewport.Y, _GraphicsDeviceManager.GraphicsDevice.Viewport.Width, _GraphicsDeviceManager.GraphicsDevice.Viewport.Height);
			}
			set {
				_GraphicsDeviceManager.GraphicsDevice.Viewport = new Viewport(value.X, value.Y, value.Width, value.Height);
			}
		}
		/// <summary>
		/// The scale at which everything is drawn.
		/// </summary>
		public Vector2 Scale {
			get {
				return _Scale;
			}
			set {
				_Scale = value;
				_Matrix = Matrix.CreateScale(Scale.X, Scale.Y, 1);
			}
		}

		private bool _Drawing;
		private GraphicsDeviceManager _GraphicsDeviceManager;
		private SpriteBatch _SpriteBatch;
		private Point _Resolution;
		private Vector2 _Scale;
		private Matrix _Matrix;

		/// <summary>
		/// Creates a new GraphicsHandler.
		/// </summary>
		/// <param name="graphicsDevice">The GraphicsDevice to use.</param>
		/// <param name="graphicsDeviceManager">The GraphicsDeviceManager to use.</param>
		public GraphicsHandler(GraphicsDeviceManager graphicsDeviceManager) {
			_Drawing = false;
			_Resolution = new Point(1280, 1020);

			_GraphicsDeviceManager = graphicsDeviceManager;
		}

		/// <summary>
		/// Must be executed upon loading content.
		/// </summary>
		public void LoadContent() {
			_SpriteBatch = new SpriteBatch(_GraphicsDeviceManager.GraphicsDevice);
			FullScreen = false;
		}

		/// <summary>
		/// Begins drawing a frame.
		/// </summary>
		public void Begin() {
			_SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _Matrix);
			_Drawing = true;
		}

		/// <summary>
		/// Ends drawing a frame and renders it.
		/// </summary>
		public void End() {
			_Drawing = false;
			_SpriteBatch.End();
		}

		/// <summary>
		/// Checks if a frame is being drawn, and if not, begins one.
		/// </summary>
		private void CheckDrawing() {
			if(!_Drawing) {
				Begin();
			}
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
			CheckDrawing();
			_SpriteBatch.Draw(texture, new RectangleF(position.X, position.Y, size.X, size.Y), tint ?? Color.White);
		}

		/// <summary>
		/// Draws some text on the screen.
		/// </summary>
		/// <param name="position">The position to draw the text at.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The font of the text to draw.</param>
		/// <param name="color">The color of the text to draw.</param>
		public void DrawText(Vector2 position, string text, SpriteFont font, Color? color = null) {
			CheckDrawing();
			_SpriteBatch.DrawString(font, text, position, color ?? Color.White);
		}

		/// <summary>
		/// Draws a rectangle on the screen.
		/// </summary>
		/// <param name="position">The position to draw the rectangle at.</param>
		/// <param name="size">The size of the rectangle.</param>
		/// <param name="color">The color of the rectangle.</param>
		public void DrawRectangle(Vector2 position, Vector2 size, Color color) {
			DrawTexture(position, size, GetColoredTexture(color, new Point(1, 1)));
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
		/// Gets a texture of the given color.
		/// </summary>
		/// <param name="color">The color.</param>
		/// <param name="dimensions">The dimensions of the texture.</param>
		/// <returns>A colored texture.</returns>
		public Texture2D GetColoredTexture(Color color, Point dimensions) {
			Texture2D texture = new Texture2D(_GraphicsDeviceManager.GraphicsDevice, dimensions.X, dimensions.Y, false, SurfaceFormat.Color);

			// Set color
			texture.SetData<Color>(new Color[] {
				color
			});

			return texture;
		}
	}
}
