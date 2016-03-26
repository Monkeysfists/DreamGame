using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class CollisionObject : PlatformTile
    {
        public CollisionObject(string name)
        {
            Texture = GameHandler.AssetHandler.GetTexture(name);
            CanCollide = true;
        }
    }
}
