using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles.Walls
{
    public class BuildingBlockWall : WallTile
    {
        public enum BuildingBlocks { wood, brick, black, ground, street, buildingblock, etc};
        public BuildingBlockWall(BuildingBlocks buildingblocks)
        {
            switch (buildingblocks){
                case BuildingBlocks.ground:
                    Texture = GameHandler.AssetHandler.GetTexture("grond");
                    break;
                case BuildingBlocks.wood:
                    Texture = GameHandler.AssetHandler.GetTexture("hout");
                    break;
                case BuildingBlocks.brick:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter1/bed");
                    break;
                case BuildingBlocks.black:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter1/bed");
                    break;
                case BuildingBlocks.street:
                    Texture = GameHandler.AssetHandler.GetTexture("stoep");
                    break;
                case BuildingBlocks.buildingblock:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter1/blokken/blok" + GameHandler.Random.Next(1,27));
                    break;
                case BuildingBlocks.etc:
                default:
                    Texture = GameHandler.AssetHandler.GetTexture("chapter1/bed");
                    break;
            }

            Size = Size * 2;
        }
    }
}
