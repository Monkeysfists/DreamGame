using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class PlantPot : PlatformTile
    {
        public PlantPot()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("");
            CanCollide = false;
        }
    }
}
