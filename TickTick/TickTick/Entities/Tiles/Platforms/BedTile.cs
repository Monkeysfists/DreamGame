using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class BedTile : PlatformTile
    {
        public BedTile(int chapter)
        {
            //TODO
            switch (chapter){
                case 2: Texture = GameHandler.AssetHandler.GetTexture("chapter3/ch2_bed"); break;
                case 3: Texture = GameHandler.AssetHandler.GetTexture("chapter3/ch3bed"); break;
                default: Texture = GameHandler.AssetHandler.GetTexture("chapter4/bed chapter 4"); break;

            }
            CanCollide = false;
        }
    }
}
