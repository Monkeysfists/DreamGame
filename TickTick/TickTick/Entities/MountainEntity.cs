using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities {
	public class MountainEntity : TextureEntity{
		/// <summary>
		/// Gives the mountain a random texture and position.
		/// </summary>
		public void Initialize() {
			Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_mountain_" + GameHandler.Random.Next(1, 3));
			ResizeToTexture();
			Size *= Layer / 4;
			Position = new Vector2((float)GameHandler.Random.NextDouble() * Parent.Size.X - Size.X / 2, Parent.Size.Y - Size.Y);
		}

		public override void Update() {
			// Parallax
			PlayerCreature player = (PlayerCreature)Parent.FindChildrenByName("player", false)[0];
			if(player.Health > 0) {
				Position += new Vector2((float)(10 - Layer) / 2500 * player.Velocity.X, (float)(10 - Layer) / 2500 * player.Velocity.Y);
			}

			base.Update();
		}
	}
}
