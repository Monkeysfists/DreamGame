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
			GraphicsHandler.Resolution = new Point(1080, 720);
			//GraphicsHandler.FullScreen = true;
			// TODO: Add fullscreen

			// Start game
			StateHandler.AddState("playing", new PlayingState());
			StateHandler.AddState("titleMenu", new TitleMenuState());
			StateHandler.AddState("helpMenu", new HelpMenuState());
			StateHandler.AddState("levelMenu", new LevelMenuState());
			StateHandler.CurrentState = StateHandler.States["titleMenu"];
			//AudioHandler.PlaySong(AssetHandler.GetSong("Sounds/snd_music"), true);
		}
	}
}
