using GameLibrary;
using TickTick.Scenes;

namespace TickTick {
	public class TickTick : GameHandler {
		static void Main() {
			TickTick game = new TickTick();
			game.Run();
		}

		protected override void Initialize() {
			base.Initialize();

			IsMouseVisible = true;
		}

		protected override void LoadContent() {
			base.LoadContent();

			Scene = new Level1();
		}
	}
}
