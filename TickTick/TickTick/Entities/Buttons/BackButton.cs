using GameLibrary;

namespace TickTick.Entities.Buttons {
	/// <summary>
	/// Returns to the main menu.
	/// </summary>
	public class BackButton : ButtonEntity {
		/// <summary>
		/// Creates a new BackButton.
		/// </summary>
		public BackButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_back");
			HoverTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_back_hover");
			ClickTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_back_click");
			Texture = DefaultTexture;
			//ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToTexture();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["titleMenu"];
		}
	}
}
