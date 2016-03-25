using GameLibrary;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TickTick.Entities.Buttons;

namespace TickTick.Entities.States {
	/// <summary>
	/// Used when a level is being played.
	/// </summary>
	public class PlayingState : State {
		/// <summary>
		/// A list of Levels.
		/// </summary>
		public ReadOnlyCollection<LevelEntity> Levels {
			get {
				List<LevelEntity> result = new List<LevelEntity>();
				
				for (int i = 0; i < Children.Count; i++) {
					if(Children[i] is LevelEntity) {
						result.Add((LevelEntity)Children[i]);
					}
				}

				return result.AsReadOnly();
			}
		}
		/// <summary>
		/// The level that is currently active.
		/// </summary>
		/// <param name="level"></param>
		public LevelEntity CurrentLevel {
			get {
				return _CurrentLevel;
			}
			set {
				if (Children.Contains(value)) {
					_CurrentLevel = value;
					CurrentLevel.Active = true;
					CurrentLevel.Visible = true;

					foreach (LevelEntity entity in Levels) {
						if (entity != CurrentLevel) {
							entity.Active = false;
							entity.Visible = false;
						}
					}
				} else {
					throw new Exception("Could not find level.");
				}
			}
		}
		/// <summary>
		/// The next level.
		/// </summary>
		public LevelEntity NextLevel {
			get {
				if(Levels.Count > CurrentLevel.LevelNumber) {
					return Levels[CurrentLevel.LevelNumber];
				} else {
					return null;
				}
			}
		}
		/// <summary>
		/// The hint.
		/// </summary>
		public HintEntity Hint;
		/// <summary>
		/// The timer.
		/// </summary>
		//public TimerEntity Timer;
		/// <summary>
		/// Whether it is game over.
		/// </summary>
		public bool GameOver {
			get {
				return _GameOver;
			}
			set {
				_GameOver = value;
				_Overlay.Visible = GameOver;
				//Timer.Active = !(GameOver || Won);
				if (GameOver) {
					_Overlay.Texture = GameHandler.AssetHandler.GetTexture("black");
					_Overlay.ResizeToTexture();
					_Overlay.Position = (Size - _Overlay.Size) / 2;
				}
			}
		}
		/// <summary>
		/// Whether the level was won.
		/// </summary>
		public bool Won {
			get {
				return _Won;
			}
			set {
				_Won = value;
				_Overlay.Visible = Won;
				//Timer.Active = !(GameOver || Won);
				if (Won) {
					_Overlay.Texture = GameHandler.AssetHandler.GetTexture("black");
					_Overlay.ResizeToTexture();
					_Overlay.Position = (Size - _Overlay.Size) / 2;
					GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("black"));
				}
			}
		}

		private bool _GameOver;
		private bool _Won;
		private LevelEntity _CurrentLevel;
		private TextureEntity _Overlay;

        /// <summary>
        /// Creates a new PlayingState.
        /// </summary>
        public PlayingState() : base("playing")
        {
            TextureEntity background = new TextureEntity();
            background.Texture = GameHandler.AssetHandler.GetTexture("chapter1/achtergrond1");
            AddChild(background);
            Size = new Vector2(GameHandler.GraphicsHandler.Resolution.X, GameHandler.GraphicsHandler.Resolution.Y);
            background.Size = Size;

            // Load levels
            for (int i = 1; i <= GameHandler.AssetHandler.CountFiles("Levels") - 1; i++)
            {
                LevelEntity level = new LevelEntity(i);
                level.Active = false;
                level.Visible = false;
                AddChild(level);
            }

            // Add quit button
            QuitButton quitButton = new QuitButton();
            quitButton.Position = new Vector2(Size.X - quitButton.Size.X - 10, 10);
            AddChild(quitButton);

            // Game over
            _Overlay = new TextureEntity();
            _Overlay.Visible = false;
            AddChild(_Overlay);
        

            /*
			// Show timer
			Timer = new TimerEntity();
			Timer.Position = new Vector2(25, 30);
			AddChild(Timer);
            */
			// Show hint

			Hint = new HintEntity();
			Hint.Position = new Vector2((Size.X - Hint.Size.X) / 2, 10);
			AddChild(Hint);

		}

		public void GoToNextLevel() {
			if (NextLevel == null) {
				GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["levelMenu"];
			} else {
				NextLevel.Locked = false;
				NextLevel.Reset();
				CurrentLevel = NextLevel;
			}
		}
	}
}
