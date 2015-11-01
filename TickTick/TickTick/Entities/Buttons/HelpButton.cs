using GameLibrary;

namespace TickTick.Entities.Buttons {
	public class HelpButton : ButtonEntity {
		/// <summary>
		/// Creates a new HelpButton.
		/// </summary>
		public HelpButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_help");
			HoverTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_help_hover");
			ClickTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_help_click");
			Texture = DefaultTexture;
            ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToTexture();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["helpMenu"];
		}
	}
}
