using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Walls
{
    public class GuitarShotgun : WallTile
    {
        public int level;
        public GuitarShotgun(int chapter)
        {
            level = chapter;
            switch (chapter){
                case 5: Texture = GameHandler.AssetHandler.GetTexture("chapter3/Gunstand");
                    break;
                default: Texture = GameHandler.AssetHandler.GetTexture("chapter4/gitaar"); 
                    break;
            }

            Velocity = Vector2.Zero;
            CanCollide = false;
            Size = new Vector2(LevelEntity.TileSize, 3*LevelEntity.TileSize);
        }

        public override void Update()
        {
            if (level == 5)
            {
                foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
                {
                    if (entity is playerCreature)
                    {
                        Texture = GameHandler.AssetHandler.GetTexture("chapter3/Emptystand");
                        CanCollide = false;
                        playerCreature.item = "shotgun";
                    }
                }
            }
        }
    }
}
