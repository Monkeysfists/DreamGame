using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.States;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Platforms
{
    public class LegoBlock : PlatformTile
    {
        public LegoBlock()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter2/legoblock_blank");
            Color = new Color(GameHandler.Random.Next(255), GameHandler.Random.Next(255), GameHandler.Random.Next(255));
        }
    }
}

