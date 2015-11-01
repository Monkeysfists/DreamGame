using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary {
	/// <summary>
	/// Can be clicked. Has specific images for different click-states.
	/// </summary>
	public class ButtonEntity : TextureEntity {
		/// <summary>
		/// The texture that is displayed by default.
		/// </summary>
		public Texture2D DefaultTexture;
		/// <summary>
		/// The texture that is displayed when the mouse hovers over the button.
		/// </summary>
		public Texture2D HoverTexture {
			get {
				if (_HoverTexture == null) {
					return DefaultTexture;
				} else {
					return _HoverTexture;
				}
			}
			set {
				_HoverTexture = value;
			}
		}
		/// <summary>
		/// The texture that is displayed when the mouse is clicking the button.
		/// </summary>
		public Texture2D ClickTexture {
			get {
				if (_ClickTexture == null) {
					return DefaultTexture;
				} else {
					return _ClickTexture;
				}
			}
			set {
				_ClickTexture = value;
			}
		}
		/// <summary>
		/// The SoundEffect to play when clicked.
		/// </summary>
		public SoundEffect ClickedSoundEffect;

		private Texture2D _HoverTexture;
		private Texture2D _ClickTexture;

		public override void Update() {
			// Check if the mouse is on top of the button
			if (GlobalBoundingBox.Contains(GameHandler.InputHandler.MousePosition)) {
				// Set correct texture
				if (GameHandler.InputHandler.MouseButtonDown(InputHandler.MouseButton.Left)) {
					// Set clicked texture
					if (Texture != ClickTexture) {
						Texture = ClickTexture;
					}

					OnMouseButtonDown();
				} else if (Texture != HoverTexture) {
					// Set hover texture
					Texture = HoverTexture;
				}

				if (GameHandler.InputHandler.OnMouseButtonHold(InputHandler.MouseButton.Left)) {
					OnMouseButtonHold();
				}
				if (GameHandler.InputHandler.OnMouseButtonUp(InputHandler.MouseButton.Left)) {
					OnMouseButtonUp();
				}

				// Play sounds
				if (GameHandler.InputHandler.OnMouseButtonDown(InputHandler.MouseButton.Left)) {
					// Play clicked sound
					if (ClickedSoundEffect != null) {
						GameHandler.AudioHandler.PlaySoundEffect(ClickedSoundEffect);
					}
				}
			} else if (Texture != DefaultTexture) {
				// Set default texture
				Texture = DefaultTexture;
			}

			base.Update();
		}

		/// <summary>
		/// Executed whenever a the button is clicked.
		/// </summary>
		protected virtual void OnMouseButtonDown() {
		}

		/// <summary>
		/// Executed whenever a click is held above the button.
		/// </summary>
		protected virtual void OnMouseButtonHold() {
		}

		/// <summary>
		/// Evecuted whenever a click is released from the button.
		/// </summary>
		protected virtual void OnMouseButtonUp() {
		}
	}
}
