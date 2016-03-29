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
			DefaultTexture = GameHandler.AssetHandler.GetTexture("Buttons/quit1");
            HoverTexture = GameHandler.AssetHandler.GetTexture("Buttons/quit2");
            ClickTexture = GameHandler.AssetHandler.GetTexture("Buttons/quit1");
			Texture = DefaultTexture;
			//ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_watercollected");

			ResizeToTexture();
            Size = Size / 2;
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["levelMenu"];
		}
	}
}
