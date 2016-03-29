using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    class Cloud : PlatformTile
    {
        public Raindrop rainDrop;

        public Cloud()
        {
            Texture = GameHandler.AssetHandler.GetTexture("wolk1");
            CanCollide = true;
            ResizeToTexture();
        }

        public override void Update()
        {

            if (GameHandler.Random.Next(1, 50) < 5)
            {
                rainDrop = new Raindrop();
                rainDrop.Position = Parent.Position + new Vector2(GameHandler.Random.Next(1,Texture.Width/rainDrop.Texture.Width)*rainDrop.Texture.Width + 2, Texture.Height);
                rainDrop.CanCollide = true;
                AddChild(rainDrop);
            }
            
            Handlecolission();
            base.Update();
        }

        public void Handlecolission()
        {
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Children), Vector2.Zero, Vector2.Zero))
            {
                if(entity is PlayerCreature)
                {
                    RemoveChild(this);
                }
            }
        }
    }
}
