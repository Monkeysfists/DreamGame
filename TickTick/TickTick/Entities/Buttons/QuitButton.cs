using GameLibrary;

namespace TickTick.Entities.Buttons {
	/// <summary>
	/// Returns to the level menu.
	/// </summary>
	public class QuitButton : ButtonEntity {
		/// <summary>
		/// Creates a new HelpButton.
		/// </summary>
		public QuitButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_quit");
			HoverTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_quit_hover");
			ClickTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_quit_click");
			Texture = DefaultTexture;
			//ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToTexture();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["levelMenu"];
		}
	}
}
