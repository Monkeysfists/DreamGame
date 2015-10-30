using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Buttons;

namespace TickTick.Entities.States {
	public class LevelMenuState : State {
		public LevelMenuState() : base("levelMenu") {
			// Add the background
			Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_levelselect");

			// Add a back button
			BackButton backButton = new BackButton();
			backButton.Position = new Vector2((GameHandler.GraphicsHandler.Resolution.X - backButton.Size.X) / 2, 750);
			AddChild(backButton);

			ResizeToContents();
		}
	}
}
