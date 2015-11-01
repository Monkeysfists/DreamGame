using Microsoft.Xna.Framework;

namespace GameLibrary {
	/// <summary>
	/// An Entity which has an animated texture.
	/// </summary>
	public class AnimatedEntity : TextureEntity {
		/// <summary>
		/// The Animation that is currently active.
		/// </summary>
		public Animation Animation {
			get {
				return _Animation;
			}
			set {
				if (value != _Animation) {
					if (_Animation != null) {
						_Animation.Cell = Point.Zero;
					}
					_Animation = value;
				}
			}
		}

		private Animation _Animation;

		public override void Update() {
			// Update animation
			Animation.Update();

			base.Update();
		}

		public override void Draw() {
			Animation.Draw(GlobalPosition - GlobalOrigin - GlobalDrawOrigin, Size);

			base.Draw();
		}
	}
}
