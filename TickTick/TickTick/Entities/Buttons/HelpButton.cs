using GameLibrary;

namespace TickTick.Entities.Buttons {
	public class HelpButton : ButtonEntity {
		public HelpButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_help");
            ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToContents();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["helpMenu"];
		}
	}
}
