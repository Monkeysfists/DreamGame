using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    class Raindrop : TextureEntity
    {
        public Raindrop()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/regendruppel");
            CanCollide = true;
            Size = new Vector2(10, 30);
        }

        public override void Update()
        {
            //if collision met object { play animatie}
            //if collision met player { player krijgt damage }
            Velocity.Y = 100F;
            base.Update();
        }

        public void Handlecolission()
        {
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is Raindrop)
                {
                    Velocity.Y = 100F;
                    RemoveChild(entity);
                }
                if(entity is PlatformTile)
                {
                    Visible = false;
                }
            }
        }
    }
}
