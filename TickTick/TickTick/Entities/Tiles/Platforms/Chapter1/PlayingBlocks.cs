using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    public class PlayingBlocks : PlatformTile
    {
        public PlayingBlocks()
        {
            //Returns a random block sprite
            Texture = GameHandler.AssetHandler.GetTexture("chapter1//blokken//blok" + GameHandler.Random.Next(27));
        }
    }
}
