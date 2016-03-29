using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    class Cloud : PlatformTile
    {
        public Cloud()
        {
            Texture = GameHandler.AssetHandler.GetTexture("wolk1");
            CanCollide = true;
            ResizeToTexture();
        }

        public override void Update()
        {
            //if collision met object { play animatie}
            //if collision met player { player krijgt damage }

            if (GameHandler.Random.Next(1, 100) < 5)
            {
                Raindrop rainDrop = new Raindrop();
                rainDrop.Position = Parent.Position + new Vector2(GameHandler.Random.Next(1,Texture.Width/rainDrop.Texture.Width)*rainDrop.Texture.Width + 2, Texture.Height);
                rainDrop.CanCollide = true;
                AddChild(rainDrop);
            }


            base.Update();
        }
    }
}
