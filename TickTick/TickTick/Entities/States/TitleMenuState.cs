using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Buttons;

namespace TickTick.Entities.States {
	public class TitleMenuState : State {
		/// <summary>
		/// Creates a new TitleMenuState.
		/// </summary>
		public TitleMenuState() : base("titleMenu") {


            // Add the background
            TextureEntity background = new TextureEntity();
			background.Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_title");
			background.ResizeToTexture();
			AddChild(background);
			ResizeToContents();

			// Add a play button
			PlayButton playButton = new PlayButton();
			playButton.Position = new Vector2((Size.X - playButton.Size.X) / 2, 460);
			AddChild(playButton);

			// Add a help button
			HelpButton helpButton = new HelpButton();
			helpButton.Position = new Vector2((Size.X - helpButton.Size.X) / 2, 570);
			AddChild(helpButton);
		}
	}
}
