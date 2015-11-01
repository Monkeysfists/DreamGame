using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary {
	/// <summary>
	/// An entity with a texture.
	/// </summary>
	public class TextureEntity : Entity {
		/// <summary>
		/// The texture of the Entity.
		/// </summary>
		public Texture2D Texture;
		/// <summary>
		/// The tint that will be applied to the texture.
		/// </summary>
		public Color Color;

		/// <summary>
		/// Creates a new TextureEntity.
		/// </summary>
		public TextureEntity() {
			Texture = GameHandler.GraphicsHandler.GetColoredPixel(Color.Transparent);
			Color = Color.White;
		}

		public override void Draw() {
			GameHandler.GraphicsHandler.DrawTexture(GlobalPosition - GlobalOrigin - GlobalDrawOrigin, GlobalRotation, Size, Texture, null, Color);

			base.Draw();
		}

		/// <summary>
		/// Sets the size to be the same as that of the texture or its contents.
		/// </summary>
		public void ResizeToTexture() {
			ResizeToContents(new Vector2(Texture.Width, Texture.Height));
		}
	}
}
