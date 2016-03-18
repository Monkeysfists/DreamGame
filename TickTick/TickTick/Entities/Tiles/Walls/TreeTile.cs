using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Walls
{
    public class TreeTile : WallTile
    {
        public int TreeAge;
        public TreeTile(int TreeAge)
        {
            //TODO
            this.TreeAge = TreeAge;
            Velocity = Vector2.Zero;

            switch (TreeAge)
            {
                case 11:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case 12:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case 21:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case 22:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case 31:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case 32:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case 41:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                default:
                    Texture = null;
                    break;
            }

            if (TreeAge != 31)
                CanCollide = true;

          
        }

        public override void Update()
        {
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is PlayerCreature)
                    GetSword();
            }
            base.Update();
        }

        public void GetSword()
        {
            CanCollide = false;
            Texture = GameHandler.AssetHandler.GetTexture("");
        }
    }
}
