using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameLibrary {
	/// <summary>
	/// Used for displaying text on screen.
	/// </summary>
	public class TextEntity : Entity {
		/// <summary>
		/// The String text.
		/// </summary>
		public String Text {
			get {
				return _Text;
			}
			set {
				_Text = value;
				Size = TextSize;
			}
		}
		/// <summary>
		/// The actual text size.
		/// </summary>
		public Vector2 TextSize {
			get {
				return Font.MeasureString(Text);
			}
		}
		/// <summary>
		/// The font.
		/// </summary>
		public SpriteFont Font {
			get {
				return _Font;
			}
			set {
				_Font = value;
				Size = TextSize;
			}
		}
		/// <summary>
		/// The color of the text.
		/// </summary>
		public Color Color;

		private String _Text;
		private SpriteFont _Font;

		/// <summary>
		/// Creates a new TextEntity.
		/// </summary>
		public TextEntity(SpriteFont font) {
			_Font = font;
			Text = "";
			Color = Color.White;
		}

		public override void Draw() {
			GameHandler.GraphicsHandler.DrawText(GlobalPosition - GlobalOrigin - GlobalDrawOrigin, GlobalRotation, Size, Text, Font, Color);

			base.Draw();
		}
	}
}
