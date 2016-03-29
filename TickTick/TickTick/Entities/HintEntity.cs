using GameLibrary;
using Microsoft.Xna.Framework;
using System;

namespace TickTick.Entities {
	/// <summary>
	/// A hint that is shown to players.
	/// </summary>
	public class HintEntity : TextureEntity {
		/// <summary>
		/// The text that is displayed on the hint.
		/// </summary>
		public string Text {
			get {
				return _Text.Text;
			}
			set {
				_Text.Text = value;
				Visible = true;
				_Shown = DateTime.Now;
			}
		}

		private TextEntity _Text;
		private DateTime _Shown;

		/// <summary>
		/// Creates a new HintEntity.
		/// </summary>
		public HintEntity() {
			Texture = GameHandler.AssetHandler.GetTexture("Overlays/spr_frame_hint");
			ResizeToTexture();

			Visible = false;
			_Text = new TextEntity(GameHandler.AssetHandler.GetSpriteFont("Fonts/HintFont"));
			_Text.Position = new Vector2(120, 25);
			_Text.Color = Color.Black;
            Name = "HintText";
			AddChild(_Text);
		}

		public override void Update() {
			if((DateTime.Now - _Shown).TotalSeconds >= 5) {
				Visible = false;
			}

			base.Update();
		}

        public Vector2 SetTextPosition
        {
            get { return _Text.Position; }
            set { _Text.Position = value; }
        }
	}
}
