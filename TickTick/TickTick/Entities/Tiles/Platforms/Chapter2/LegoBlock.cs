using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class LegoBlock : PlatformTile
    {
        public LegoBlock()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter2/legoblock_blank");
            //TODO: random color
        }
    }
}

