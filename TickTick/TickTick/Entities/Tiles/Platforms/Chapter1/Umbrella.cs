using GameLibrary;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TickTick.Entities.Tiles.Platforms.Chapter1;
using TickTick.Entities.Tiles.Creatures;
using TickTick.Animations;

namespace TickTick.Entities.Tiles.Platforms
{
    public class Umbrella : PlatformTile
    {
        public Umbrella()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/umbrella");
            Size = new Vector2(Texture.Width * LevelEntity.TileSize/20, Texture.Height * LevelEntity.TileSize/20);
            CanCollide = true;

        }

        public override void Update()
        {
            Handlecolission();
        }

        public void Handlecolission()
        {
        }
    }
}