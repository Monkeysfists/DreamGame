using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class Umbrella : PlatformTile
    {
        public Umbrella()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/umbrella");
            CanCollide = true;
        }

        public void Update()
        {
            //TODO: Collision met regendruppels
        }
    }
}