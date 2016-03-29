using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Tiles.Platforms.Chapter1;
using GameLibrary.Types;

namespace TickTick.Entities.Tiles.Platforms
{
    public class TrampolineBedTile : PlatformTile
    {
        public TrampolineBedTile()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/bed");
            CanCollide = true;
            Size = new Vector2(Texture.Width, Texture.Height);
            /*
            CanCollide = true;

            CollisionBox = new RectangleF(Position.X, Position.Y, Texture.Width, Texture.Height);
            */
        }
    }
}
