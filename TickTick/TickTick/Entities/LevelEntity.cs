using GameLibrary;

namespace TickTick.Entities {
	public class LevelEntity : Entity {
		public bool Locked;
		public bool Solved;

		public LevelEntity(int level) {
			// Add the background
			Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_sky");

			// TODO: Load level
			Locked = true; // TODO: Change
			Solved = false; // TODO: Change

			ResizeToContents();
		}
	}
}
