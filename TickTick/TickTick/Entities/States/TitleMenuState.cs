using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Buttons;

namespace TickTick.Entities.States {
	public class TitleMenuState : State {
		public TitleMenuState() : base("titleMenu") {
			// Add the background
			Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_title");
			ResizeToContents();

			// Add a play button
			PlayButton playButton = new PlayButton();
			playButton.Position = new Vector2((GameHandler.GraphicsHandler.Resolution.X - playButton.Size.X) / 2, 540);
			AddChild(playButton);

			// Add a help button
			HelpButton helpButton = new HelpButton();
			helpButton.Position = new Vector2((GameHandler.GraphicsHandler.Resolution.X - helpButton.Size.X) / 2, 600);
			AddChild(helpButton);
		}
	}
}
