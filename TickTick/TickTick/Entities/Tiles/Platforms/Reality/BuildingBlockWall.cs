using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class BuildingBlockWall : PlatformTile
    {
        public enum BuildingBlocks { wood, brick, black, ground, street, etc};
        public BuildingBlockWall(BuildingBlocks buildingblocks)
        {
            //TODO add textures
            switch (buildingblocks){
                case BuildingBlocks.wood:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case BuildingBlocks.brick:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case BuildingBlocks.black:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
                case BuildingBlocks.etc:
                default:
                    Texture = GameHandler.AssetHandler.GetTexture("");
                    break;
            }
        }
    }
}
