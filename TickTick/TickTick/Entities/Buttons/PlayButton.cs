using GameLibrary;

namespace TickTick.Entities.Buttons {
	/// <summary>
	/// Goes to the level select screen.
	/// </summary>
	public class PlayButton : ButtonEntity {
		/// <summary>
		/// Creates a new PlayButton.
		/// </summary>
		public PlayButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_play");
			HoverTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_play_hover");
			ClickTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_button_play_click");
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
