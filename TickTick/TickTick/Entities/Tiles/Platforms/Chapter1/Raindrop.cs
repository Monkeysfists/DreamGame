using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    class Raindrop : PlatformTile
    {
        public Raindrop()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/regendruppel");
            CanCollide = true;
        }

        public void Update()
        {
            //if collision met object { play animatie}
            //if collision met player { player krijgt damage }
        }
    }
}
