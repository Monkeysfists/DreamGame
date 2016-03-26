using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.States;

namespace TickTick.Entities.Buttons {
	/// <summary>
	/// Starts a level.
	/// </summary>
	public class LevelButton : ButtonEntity {
		/// <summary>
		/// The associated level.
		/// </summary>
		public LevelEntity Level {
			get {
				return _Level;
			}
			set {
				_Level = value;
				SetStateTexture();
				_LevelText.Text = (_Level.LevelNumber).ToString();
				_LevelText.Position = new Vector2(Size.X - _LevelText.Size.X - 10, 5);
			}

		}

		private LevelEntity _Level;
		private TextEntity _LevelText;

		/// <summary>
		/// Creates a new LevelButton.
		/// </summary>
		/// <param name="level">The level to associate with the button.</param>
		public LevelButton(LevelEntity level) {
			_LevelText = new TextEntity(GameHandler.AssetHandler.GetSpriteFont("Fonts/Hud"));
			AddChild(_LevelText);

			//ClickedSoundEffect = GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_won");

			Level = level;

		}

		public override void Update() {
			base.Update();

			SetStateTexture();
		}

		protected override void OnMouseButtonUp() {
			base.OnMouseButtonUp();

			if(!Level.Locked) {
				GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["playing"];
				((PlayingState)GameHandler.StateHandler.States["playing"]).CurrentLevel = Level;
				Level.Reset();
			}
        }

		/// <summary>
		/// Sets the texture to that of the state of the level.
		/// </summary>
		private void SetStateTexture() {
			if (Level.Locked) {
				if (DefaultTexture != GameHandler.AssetHandler.GetTexture("Sprites/spr_level_locked")) {
					DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_locked");
					HoverTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_locked_hover");
					ClickTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_locked_click");
					Texture = DefaultTexture;
					ResizeToTexture();
				}
			} else {
				if (Level.Solved) {
					if (DefaultTexture != GameHandler.AssetHandler.GetTexture("Sprites/spr_level_solved")) {
						DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_solved");
						HoverTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_solved_hover");
						ClickTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_solved_click");
						Texture = DefaultTexture;
						ResizeToTexture();
					}
				} else {
					if (DefaultTexture != GameHandler.AssetHandler.GetTexture("Sprites/spr_level_unsolved")) {
						DefaultTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_unsolved");
						HoverTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_unsolved_hover");
						ClickTexture = GameHandler.AssetHandler.GetTexture("Sprites/spr_level_unsolved_click");
						Texture = DefaultTexture;
						ResizeToTexture();
					}
				}
			}

			Size = new Vector2(108, 108);
		}
	}
}
