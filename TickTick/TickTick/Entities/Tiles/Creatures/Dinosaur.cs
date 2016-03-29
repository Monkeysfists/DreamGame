using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace TickTick.Entities.Tiles.Creatures
{
    class Dinosaur : CreatureTileEntity
    {
        SpriteSheet SpriteSheet;

        public Dinosaur()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("chapter2/dinosaur");
        }

        public void Update()
        {
            if (GameHandler.Random.Next(100) == 0)
            {
                //mirror
            }
        }
    }
}
