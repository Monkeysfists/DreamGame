using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Buttons;

namespace TickTick.Entities.States {
	public class HelpMenuState : State {
		public HelpMenuState() : base("helpMenu") {
			// Add the background
			Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_help");

			// Add a back button
			BackButton backButton = new BackButton();
			backButton.Position = new Vector2((GameHandler.GraphicsHandler.Resolution.X - backButton.Size.X) / 2, 750);
			AddChild(backButton);

			ResizeToContents();
		}
	}
}
