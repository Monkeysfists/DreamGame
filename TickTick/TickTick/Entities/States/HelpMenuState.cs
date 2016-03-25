using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Buttons;

namespace TickTick.Entities.States {
	public class HelpMenuState : State {
		/// <summary>
		/// Creates a new HelpMenuState.
		/// </summary>
		public HelpMenuState() : base("helpMenu") {

            // Add the background
            TextureEntity background = new TextureEntity();
			background.Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_help");
			background.ResizeToTexture();
			AddChild(background);
			ResizeToContents();

			// Add a back button
			BackButton backButton = new BackButton();
			backButton.Position = new Vector2((Size.X - backButton.Size.X) / 2, 750);
			AddChild(backButton);
		}
	}
}
