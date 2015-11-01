using GameLibrary;
using System;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// A flame creature that stalks the player.
	/// </summary>
	public class StalkerFlame : FlameCreature {
		public override void Update() {
			PlayerCreature player = (PlayerCreature)Parent.FindChildrenByName("player", true)[0];
			float direction = player.Position.X - Position.X;
			if (Math.Sign(direction) != Math.Sign(Velocity.X) && player.Velocity.X != 0.0f && Velocity.X != 0.0f) {
				TurnAround();
			}

			base.Update();
		}
	}
}
