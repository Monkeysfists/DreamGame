using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Walls
{
    public class TreeTile : WallTile
    {
        int level;

        public TreeTile(int level)
        {
            Velocity = Vector2.Zero;
            this.level = level;

            switch (level)
            {
                case 2:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter1/ch1_tree");
                    Size = new Vector2(LevelEntity.TileSize * 2, LevelEntity.TileSize * 4);
                    CanCollide = false;
                    break;
                case 4:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter2/ch2_tree");
                    Size = new Vector2(LevelEntity.TileSize*6,LevelEntity.TileSize*12);
                    CanCollide = false;
                    break;
                case 8:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter3/ch3_tree_sword");
                    Size = new Vector2(LevelEntity.TileSize * 6, LevelEntity.TileSize * 12);
                    CanCollide = true;
                    break;
                default:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter4/ch4_tree");
                    Size = new Vector2(LevelEntity.TileSize * 6, LevelEntity.TileSize * 12);
                    CanCollide = false;
                    break;
            }
        }
    }
}
