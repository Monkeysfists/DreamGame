using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Animations;
using GameLibrary.Types;
using Microsoft.Xna.Framework.Graphics;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Platforms
{
    public class PCDesk : PlatformTile
    {
        private float UpdateTimer;
        private int level;

        public PCDesk(int level)
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("chapter3/computer");
            UpdateTimer = 0;
            this.level = level;
        }

        public override void Update()
        {
            if(UpdateTimer > 1)
            {
                UpdateTimer += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                UpdateTimer = 0;
            }
            base.Update();
        }
    }
}
