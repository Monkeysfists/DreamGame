using GameLibrary;
using System;

namespace TickTick.Entities.Tiles.Creatures {
	/// <summary>
	/// An unpredictable flame creature.
	/// </summary>
	public class WildFlame : FlameCreature {
		public override void Update() {
			if(_WaitTime < 0 && GameHandler.Random.NextDouble() < 0.01) {
				TurnAround();
				Velocity.X = Math.Sign(Velocity.X) * (float)GameHandler.Random.NextDouble() * 5F;
			}

			base.Update();
		}
	}
}
