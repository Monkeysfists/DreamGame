using GameLibrary;

namespace TickTick {
	public class TickTick : GameHandler {
		static void Main() {
			TickTick game = new TickTick();
			game.Run();
		}

		public TickTick() {
			IsMouseVisible = true;
		}
	}
}
