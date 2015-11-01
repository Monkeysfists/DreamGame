using GameLibrary;
using System;

namespace TickTick.Animations {
	/// <summary>
	/// An electrocuting Sparky.
	/// </summary>
	public class SparkyElectrocuteAnimation : Animation {
		/// <summary>
		/// Creates a new SparkyElectrocuteAnimation.
		/// </summary>
		public SparkyElectrocuteAnimation() {
			SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("Sprites/Sparky/spr_electrocute@6x5");
			FrameTime = TimeSpan.FromMilliseconds(100);
			Loop = false;
		}
	}
}
