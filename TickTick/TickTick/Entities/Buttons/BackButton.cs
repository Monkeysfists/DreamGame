using GameLibrary;

namespace TickTick.Entities.Buttons {
	public class BackButton : ButtonEntity {
		public BackButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_back");
            ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToContents();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["titleMenu"];
		}
	}
}
