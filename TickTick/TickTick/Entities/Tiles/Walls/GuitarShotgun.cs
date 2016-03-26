using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles.Walls
{
    public class GuitarShotgun : WallTile
    {
        public GuitarShotgun()
        {
            //TODO
            
            Texture = GameHandler.AssetHandler.GetTexture("");
            Velocity = Vector2.Zero;
            //Add collision playerCreature

        }
    }
}
