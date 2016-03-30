using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class TrainTracks : PlatformTile
    {
        public TrainTracks()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/traintrack");
            Size = new Microsoft.Xna.Framework.Vector2(LevelEntity.TileSize, LevelEntity.TileSize);
        }
    }
}
