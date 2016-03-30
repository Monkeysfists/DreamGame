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

        public void DrawTexture(Vector2 position, float rotation, Vector2 size, Texture2D texture, RectangleF section, object p1, object spriteEffects, object p2)
        {
            throw new NotImplementedException();
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
		/// <summary>
		/// The amount of frames per second.
		/// </summary>
		public int Fps {
			get {
				return _Fps;
			}
		}
		
		private int _Fps;
		private DateTime _DrawStart;
		private int _Frames;
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
			_DrawStart = DateTime.Now;
			_Fps = 0;
			_Frames = 0;
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
			// Clear screen
			Clear(Color.LightBlue);
			_SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _Matrix);
		}

		/// <summary>
		/// Ends drawing a frame and renders it.
		/// </summary>
		public void End() {
			_SpriteBatch.End();

			// Calculate FPS
			if ((DateTime.Now - _DrawStart).TotalMilliseconds < 1000) {
				_Frames++;
			} else {
				_Fps = _Frames;
				_DrawStart = DateTime.Now;
				_Frames = 0;
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
		/// <param name="rotation">How much to rotate the drawn texture.</param>
		/// <param name="size">The size of the texture.</param>
		/// <param name="texture">The texture to draw.</param>
		/// <param name="section">The part of the texture to draw.</param>
		/// <param name="tint">The tint to apply to the texture.</param>
		/// <param name="effects">Effects to apply to the texture.</param>
		public void DrawTexture(Vector2 position, float rotation, Vector2 size, Texture2D texture, RectangleF section = null, Color? tint = null, SpriteEffects effects = SpriteEffects.None) {
			// Draws using a Vector to avoid per-pixel movement. Calculates scale based on section size.
			if(section == null) {
				section = new RectangleF(0, 0, texture.Width, texture.Height);
            }
			_SpriteBatch.Draw(texture, position, section, tint ?? Color.White, rotation, Vector2.Zero, new Vector2(size.X / section.Width, size.Y / section.Height), effects, 0F);
		}

		/// <summary>
		/// Draws a part of a SpriteSheet on the screen.
		/// </summary>
		/// <param name="position">The position to draw the extracted part at</param>
		/// <param name="size">The size of the extracted part.</param>
		/// <param name="cell">The cell of the SpriteSheet to extract.</param>
		/// <param name="spriteSheet">The SpriteSheet.</param>
		/// <param name="effects">Effects to apply to the texture.</param>
		public void DrawSpriteSheet(Vector2 position, Vector2 size, Point cell, SpriteSheet spriteSheet, Color? tint = null, SpriteEffects effects = SpriteEffects.None) {
			DrawTexture(position, 0F, size, spriteSheet.Texture, new RectangleF(cell.X * spriteSheet.CellSize.X, cell.Y * spriteSheet.CellSize.Y, spriteSheet.CellSize.X, spriteSheet.CellSize.Y), tint, effects);
		}

		/// <summary>
		/// Draws some text on the screen.
		/// </summary>
		/// <param name="position">The position to draw the text at.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The font of the text to draw.</param>
		/// <param name="color">The color of the text to draw.</param>
		/// <param name="effects">Effects to apply to the texture.</param>
		public void DrawText(Vector2 position, float rotation, Vector2 size, String text, SpriteFont font, Color? color = null, SpriteEffects effects = SpriteEffects.None) {
			_SpriteBatch.DrawString(font, text, position, color ?? Color.White, rotation, Vector2.Zero, new Vector2(size.X / font.MeasureString(text).X, size.Y / font.MeasureString(text).Y), SpriteEffects.None, 0F);
		}

		/// <summary>
		/// Draws a rectangle on the screen.
		/// </summary>
		/// <param name="position">The position to draw the rectangle at.</param>
		/// <param name="rotation">The amount to rotate the rectangle.</param>
		/// <param name="size">The size of the rectangle.</param>
		/// <param name="color">The color of the rectangle.</param>
		public void DrawRectangle(Vector2 position, Vector2 size, Color color) {
			DrawTexture(position, 0F, size, GetColoredPixel(color));
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
		/// Gets an empty texture.
		/// </summary>
		/// <param name="size">The size of the texture.</param>
		/// <returns>An empty texture.</returns>
		public Texture2D GetEmptyTexture(Point size) {
			return new Texture2D(_GraphicsDeviceManager.GraphicsDevice, size.X, size.Y);
		}

		/// <summary>
		/// Gets a texture of the given colors.
		/// </summary>
		/// <param name="colorData">The colors.</param>
		/// <param name="size">The size of the texture.</param>
		/// <returns>A colored texture.</returns>
		public Texture2D GetColoredTexture(Color[] colorData, Point size) {
			Texture2D result = GetEmptyTexture(size);

			// Set color
			result.SetData<Color>(colorData);

			return result;
		}

		/// <summary>
		/// Gets a 1x1 texture of the given color.
		/// </summary>
		/// <param name="color">The color.</param>
		/// <returns>A colored 1x1 texture.</returns>
		public Texture2D GetColoredPixel(Color color) {
			return GetColoredTexture(new Color[] {
				color
			}, new Point(1, 1));
		}
	}
}
