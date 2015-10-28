using GameLibrary;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TickTick.Scenes {
	public class Level1 : Entity {
		public Level1() {
			Entity background = new Entity();
			background.Texture = GameHandler.AssetHandler.GetTexture("spr_sky");
			background.Position = Vector2.Zero;
			background.Size = new Vector2(100, 100);
			AddChild(background);
		}
	}
}
