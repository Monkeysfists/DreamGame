using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class HighSchool : PlatformTile
    {
        public HighSchool(int chapter)
        {
            //TODO
            switch (chapter)
            {
                case 3: Texture = GameHandler.AssetHandler.GetTexture("chapter3/schoolchapter3");
                    break;
                default: Texture = GameHandler.AssetHandler.GetTexture("chapter4/school_hek");
                    break;
            }
            CanCollide = false;
        }
    }
}
