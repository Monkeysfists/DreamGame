using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class TrainStopBlock : PlatformTile
    {
        public TrainStopBlock()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("chapter1//treinstop");
            CanCollide = true;
            Size = new Microsoft.Xna.Framework.Vector2(LevelEntity.TileSize*2,LevelEntity.TileSize*2);
        }
    }
}
