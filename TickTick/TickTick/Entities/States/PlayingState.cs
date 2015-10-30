using GameLibrary;

namespace TickTick.Entities.States {
	public class PlayingState : State {
		public PlayingState() : base("playing") {
			// Load levels
			for(int i = 0; i < 10; i++) {
				LevelEntity level = new LevelEntity(i);
				
			}

			ResizeToContents();
		}
	}
}
