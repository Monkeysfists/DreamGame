using GameLibrary;

namespace TickTick.Entities.Tiles.Walls
{
    public class PlayingBlocks : WallTile
    {
        public PlayingBlocks()
        {
            //Returns a random block sprite
            Texture = GameHandler.AssetHandler.GetTexture("chapter1//blokken//blok" + GameHandler.Random.Next(1,27));
        }
    }
}
