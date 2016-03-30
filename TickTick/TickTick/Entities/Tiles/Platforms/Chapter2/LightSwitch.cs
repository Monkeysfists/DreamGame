using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Creatures;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Platforms.Chapter2
{
    public class LightSwitch : WallTile
    {
        public LightSwitch()
        {

            Texture = GameHandler.AssetHandler.GetTexture("chapter2/lightswitch_down");

            Velocity = Vector2.Zero;
            CanCollide = false;
            Size = new Vector2(20, 60);
        }

        public override void Update()
        {
            base.Update();

            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is PlayerCreature)
                {
                    Texture = GameHandler.AssetHandler.GetTexture("chapter2/lightswitch_up");
                }
            }
        }
    }
}
