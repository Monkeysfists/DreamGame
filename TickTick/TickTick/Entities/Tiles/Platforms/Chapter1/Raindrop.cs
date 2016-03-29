using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    class Raindrop : TextureEntity
    {
        public Raindrop()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/regendruppel");
            CanCollide = true;
            Size = new Vector2(10, 30);
            Velocity.Y = 100F * (1 + (GameHandler.Random.Next(1, 5) / 10));
        }

        public override void Update()
        {
            Handlecolission();
            base.Update();
        }

        public void Handlecolission()
        {
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(RootParent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is Raindrop)
                {
                    Velocity.Y = 100F;
                    RemoveChild(entity);
                }else
                if(entity is PlatformTile)
                {
                    Visible = false;
                }else
                if(entity is PlayerCreature)
                {
                    Visible = false;
                }
            }
        }
    }
}
