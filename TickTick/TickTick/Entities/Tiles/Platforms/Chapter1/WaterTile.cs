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

namespace TickTick.Entities.Tiles.Platforms
{
    public class WaterTile : PlatformTile
    {
        public Animation waterAnimation;

        public WaterTile()
        {
            waterAnimation = new WaterAnimation();
            //SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter1/water");
            GameHandler.AssetHandler.GetSpriteSheet("chapter1/water");
        }

        public void Update()
        {
            //this.Animation = waterAnimation;
        }
    }
}