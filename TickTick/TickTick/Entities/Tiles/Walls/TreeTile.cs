using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Walls
{
    public class TreeTile : WallTile
    {

        public TreeTile(int level)
        {
            Velocity = Vector2.Zero;

            switch (level)
            {
                case 2:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter1/ch1_tree");
                    break;
                case 4:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter2/ch2_tree");
                    break;
                case 8:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter3/ch3_tree_sword");
                    break;
                default:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter4/ch4_tree");
                    break;
            }

            CanCollide = true;
        }

        public override void Update()
        {
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is playerCreature)
                {
                    CanCollide = false;
                    Texture = GameHandler.AssetHandler.GetTexture("chapter3/ch3_tree_swordless");
                    playerCreature.item = "sword";
                }
            }
            base.Update();
        }
    }
}
