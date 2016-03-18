using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class BedTile : PlatformTile
    {
        public BedTile()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("");
            CanCollide = false;
        }
    }
}
