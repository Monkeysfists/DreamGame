using GameLibrary;

namespace TickTick.Entities.Buttons {
	public class LevelButton : ButtonEntity {
		public LevelEntity Level;

		public LevelButton(int level) {
			Level = new LevelEntity(level);

			if(Level.Locked) {
				DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_locked");
			} else {
				if(Level.Solved) {
					DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_solved");
				} else {
					DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_unsolved");
				}
			}

			ResizeToContents();
		}
	}
}
