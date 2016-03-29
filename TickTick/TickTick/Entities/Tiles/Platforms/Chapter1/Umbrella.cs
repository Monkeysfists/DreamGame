using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Platforms.Chapter1;

namespace TickTick.Entities.Tiles.Platforms
{
    public class Umbrella : PlatformTile
    {
        public Umbrella()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/umbrella");
            //Size = new Vector2(Texture.Width, Texture.Height);
            Size *= 5;

            CanCollide = true;

        }

        public override void Update()
        {
            //TODO: Collision met regendruppels
            //base.Update();
            Handlecolission();
        }

        public void Handlecolission()
        {
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Parent.Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is Raindrop || entity is Cloud)
                {
                    RemoveChild(entity);
                }
            }
        }
    }
}