using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.States;

namespace TickTick {
	public class TickTick : GameHandler {
		static void Main() {
			TickTick game = new TickTick();
			game.Run();
		}

		protected override void LoadContent() {
			base.LoadContent();

			// Set up environment
			IsMouseVisible = true;
			//GraphicsHandler.FullScreen = true;
			// TODO: Enable fullscreen
			GraphicsHandler.Resolution = new Point(1440, 825);
            GraphicsHandler.FullScreen = true;

			// Start game
			StateHandler.AddState("titleMenu", new TitleMenuState());
			StateHandler.AddState("helpMenu", new HelpMenuState());
			StateHandler.AddState("levelMenu", new LevelMenuState());
			StateHandler.CurrentState = StateHandler.States["titleMenu"];
			AudioHandler.PlaySong(AssetHandler.GetSong("Sounds/snd_music"), true);
		}
	}
}
