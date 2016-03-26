using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.Tiles.Walls
{
    public class GuitarShotgun : WallTile
    {
        public GuitarShotgun(int chapter)
        {
            switch (chapter){
                case 3: Texture = GameHandler.AssetHandler.GetTexture("chapter3/Gunstand");
                        //TODO: IF GUN=PICKED UP: Texture = GameHandler.AssetHandler.GetTexture("chapter3/Emptystand"); 
                        break;
                default: Texture = GameHandler.AssetHandler.GetTexture("chapter4/gitaar"); 
                    break;

            }

            Velocity = Vector2.Zero;
            //Add collision playerCreature

        }
    }
}
