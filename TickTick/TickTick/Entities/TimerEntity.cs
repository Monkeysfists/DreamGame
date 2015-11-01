using GameLibrary;
using Microsoft.Xna.Framework;
using System;
using TickTick.Entities.States;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities {
	/// <summary>
	/// A timer that is shown to players.
	/// </summary>
	public class TimerEntity : TextureEntity {
		/// <summary>
		/// The time multiplier.
		/// </summary>
		public double Multiplier;
		/// <summary>
		/// The time that's left.
		/// </summary>
		public TimeSpan TimeLeft;

		private TextEntity _Time;

		/// <summary>
		/// Creates a new TimerEntity.
		/// </summary>
		public TimerEntity() {
			Texture = GameHandler.AssetHandler.GetTexture("Sprites/spr_timer");
			ResizeToTexture();

			_Time = new TextEntity(GameHandler.AssetHandler.GetSpriteFont("Fonts/Hud"));
			AddChild(_Time);

			Multiplier = 1F;
			TimeLeft = new TimeSpan();
		}

		public override void Update() {
			double totalSeconds = GameHandler.GameTime.ElapsedGameTime.TotalSeconds * Multiplier;
			TimeLeft -= TimeSpan.FromSeconds(totalSeconds);

			if (TimeLeft.Ticks >= 0) {
				DateTime timeLeft = new DateTime(TimeLeft.Ticks);
				_Time.Text = timeLeft.ToString("mm:ss");
				_Time.Position = (Size - _Time.Size) / 2;

				_Time.Color = Color.Yellow;

				if (TimeLeft.TotalSeconds <= 10 && (int)TimeLeft.TotalSeconds % 2 == 0) { // Using modulo for flashing time here, GENIUS :)
					_Time.Color = Color.Red;
				}
			} else {
				PlayerCreature player = (PlayerCreature)((PlayingState)Parent).CurrentLevel.FindChildrenByName("player", false)[0];
				player.Explode();
			}

			base.Update();
		}
	}
}
