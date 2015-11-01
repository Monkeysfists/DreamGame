using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities {
	public class CloudEntity : TextureEntity {
		public override void Update() {
			// Parallax
			PlayerCreature player = (PlayerCreature)Parent.FindChildrenByName("player", false)[0];
			if (player.Health > 0) {
				Position += new Vector2((float)(10 - Layer) / 2500 * player.Velocity.X, (float)(10 - Layer) / 2500 * player.Velocity.Y);
			}

			if ((Velocity.X < 0F && Position.X + Size.X < 0F) || (Velocity.X > 0F && Position.X > Parent.Size.X)) {
				Reset();
			}

			base.Update();
		}

		/// <summary>
		/// Sets the start positions.
		/// </summary>
		public void Initialize() {
			Reset();
			Position = new Vector2((float)GameHandler.Random.NextDouble() * Parent.Size.X - Size.X / 2 - 500, (float)GameHandler.Random.NextDouble() * Parent.Size.Y - Size.Y / 2 - 500);
		}

		/// <summary>
		/// Gives the cloud a random texture and position.
		/// </summary>
		public void Reset() {
			Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_cloud_" + GameHandler.Random.Next(1, 6));
			ResizeToTexture();

			Size *= Layer / 4;

			Velocity = new Vector2((float)((GameHandler.Random.NextDouble() * 2) - 1) * 20, 0);

			float cloudHeight = (float)GameHandler.Random.NextDouble() * (Parent.Size.Y - 500) - Size.Y / 2;

			if (Velocity.X < 0) {
				Position = new Vector2(Parent.Size.X, cloudHeight + 500);
			} else {
				Position = new Vector2(-Size.X, cloudHeight + 500);
			}
		}
	}
}
