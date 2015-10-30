using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary {
	public class ButtonEntity : Entity {
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
		public Texture2D ClickedTexture {
			get {
				if(_ClickedTexture == null) {
					return DefaultTexture;
				} else {
					return _ClickedTexture;
				}
			}
			set {
				_ClickedTexture = value;
			}
		}
		/// <summary>
		/// The SoundEffect to play when hovered.
		/// </summary>
		public SoundEffect HoverSoundEffect;
		/// <summary>
		/// The SoundEffect to play when clicked.
		/// </summary>
		public SoundEffect ClickedSoundEffect;

		private Texture2D _HoverTexture;
		private Texture2D _ClickedTexture;

		public override void Update() {
			base.Update();

			if(Active) {
				// Check if the mouse is on top of the button
				if (GlobalHitbox.Contains(GameHandler.InputHandler.MousePosition)) {
					// Set correct texture
					if (GameHandler.InputHandler.OnMouseButtonDown(InputHandler.MouseButton.Left)) {
						// Set clicked texture
						if (Texture != ClickedTexture) {
							Texture = ClickedTexture;
						}

						// Play clicked sound
						if (ClickedSoundEffect != null) {
							GameHandler.AudioHandler.PlaySoundEffect(ClickedSoundEffect);
						}

						OnMouseButtonDown();
					} else if (Texture != HoverTexture) {
						// Set hover texture
						Texture = HoverTexture;

						// Play hover sound
						if (HoverSoundEffect != null) {
							GameHandler.AudioHandler.PlaySoundEffect(HoverSoundEffect);
						}
					} else if (Texture != DefaultTexture) {
						// Set default texture
						Texture = DefaultTexture;
					}


					if (GameHandler.InputHandler.OnMouseButtonHold(InputHandler.MouseButton.Left)) {
						OnMouseButtonHold();
					}
					if (GameHandler.InputHandler.OnMouseButtonUp(InputHandler.MouseButton.Left)) {
						OnMouseButtonUp();
					}
				}
			}
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
