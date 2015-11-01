using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameLibrary {
	public class Animation {
		/// <summary>
		/// The SpriteSheet that is used.
		/// </summary>
		public SpriteSheet SpriteSheet;
		/// <summary>
		/// The current cell of the SpriteSheet.
		/// </summary>
		public Point Cell;
		/// <summary>
		/// The duration of a frame, in seconds. Set to 0 to disable animation.
		/// </summary>
		public TimeSpan FrameTime;
		/// <summary>
		/// Whether to flip the sprites horizontally.
		/// </summary>
		public bool FlipHorizontally;
		/// <summary>
		/// Whether to flip the sprites vertically.
		/// </summary>
		public bool FlipVertically;
		/// <summary>
		/// Whether the animation should loop.
		/// </summary>
		public bool Loop;

		private DateTime _LastFrame;

		/// <summary>
		/// Creates a new Animation.
		/// </summary>
		public Animation() {
			Cell = new Point(0, 0);
			_LastFrame = DateTime.Now;
			Loop = true;

			FrameTime = new TimeSpan();
		}

		public void Update() {
			// Update frame
			if ((DateTime.Now - _LastFrame) >= FrameTime) {
				Cell.X++;
				if (Cell.X >= SpriteSheet.Dimensions.X) {
					Cell.X = 0;
					Cell.Y++;
					if(Cell.Y >= SpriteSheet.Dimensions.Y) {
						if(Loop) {
							Cell.Y = 0;
						} else {
							Cell = new Point(SpriteSheet.Dimensions.X - 1, SpriteSheet.Dimensions.Y - 1);
						}
					}
				}

				_LastFrame = DateTime.Now;
			}
		}

		public void Draw(Vector2 position, Vector2 size) {
			// Draw frame
			SpriteEffects effects = SpriteEffects.None;
			if (FlipHorizontally) {
				effects |= SpriteEffects.FlipHorizontally;
			}
			if (FlipVertically) {
				effects |= SpriteEffects.FlipVertically;
			}
			GameHandler.GraphicsHandler.DrawSpriteSheet(position, size, Cell, SpriteSheet, null, effects);
		}
	}
}
