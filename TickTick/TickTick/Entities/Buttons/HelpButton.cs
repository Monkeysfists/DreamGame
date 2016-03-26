using GameLibrary;

namespace TickTick.Entities.Buttons {
	public class HelpButton : ButtonEntity {
		/// <summary>
		/// Creates a new HelpButton.
		/// </summary>
		public HelpButton() {
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Buttons/options1");
            HoverTexture = GameHandler.AssetHandler.GetTexture("Buttons/options2");
            ClickTexture = GameHandler.AssetHandler.GetTexture("Buttons/options1");
			Texture = DefaultTexture;
            //ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToTexture();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["helpMenu"];
		}
	}
}
