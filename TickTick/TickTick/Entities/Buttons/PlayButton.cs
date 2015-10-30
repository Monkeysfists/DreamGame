using GameLibrary;

namespace TickTick.Entities.Buttons {
	public class PlayButton : ButtonEntity {
		public PlayButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_play");
            ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToContents();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["levelMenu"];
		}
	}
}
