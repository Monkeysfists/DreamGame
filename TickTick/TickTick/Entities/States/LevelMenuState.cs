using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Buttons;

namespace TickTick.Entities.States {
	public class LevelMenuState : State {
		/// <summary>
		/// Creates a new LevelMenuState.
		/// </summary>
		public LevelMenuState() : base("levelMenu") {
			// Add the background
			TextureEntity background = new TextureEntity();
			background.Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_levelselect");
			background.ResizeToTexture();
			AddChild(background);
			ResizeToContents();

			// Add a back button
			BackButton backButton = new BackButton();
			backButton.Position = new Vector2((GameHandler.GraphicsHandler.Resolution.X - backButton.Size.X) / 2, 750);
			AddChild(backButton);

			// Load buttons
			for(int i = 0; i < ((PlayingState)GameHandler.StateHandler.States["playing"]).Levels.Count; i++) {
				int row = i / 4;
				int column = i % 4;

				LevelButton button = new LevelButton(((PlayingState)GameHandler.StateHandler.States["playing"]).Levels[i]);
				button.Position = new Vector2(column * (button.Size.X + 20F), row * (button.Size.Y + 20F)) + new Vector2(390F, 180F);
				AddChild(button);
			}
		}
	}
}
