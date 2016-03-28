﻿using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    public class Board : PlatformTile
    {
        public NoCollisionObject pole1, pole2;

        public Board()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/uithangborden/bord" + GameHandler.Random.Next(1, 4));
            pole1 = new NoCollisionObject("chapter1/uithangborden/bordpaal1");
            pole2 = new NoCollisionObject("chapter1/uithangborden/bordpaal2");
            pole1.Layer = 1;
            pole2.Layer = 500;
            pole1.Position = this.Position;
            pole2.Position = this.Position;
            this.AddChild(pole1);
            this.AddChild(pole2);
            //TODO: collision met player
        }
    }
}