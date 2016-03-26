using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class NoCollisionObject : PlatformTile
    {
        public NoCollisionObject(string name)
        {
            Texture = GameHandler.AssetHandler.GetTexture(name);
            CanCollide = false;
        }
    }
}
