﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;
using TickTick.Entities.Tiles.Creatures;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    class Raindrop : TileEntity
    {
        public Vector2 Startposition;
        public float DelayTimer;
        public bool Reset;
        public Raindrop()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/regendruppel");
            CanCollide = true;
            Size = new Vector2(10, 30);
            Velocity.Y = 350F * (1 + (GameHandler.Random.Next(1, 5) / 10));
            Startposition = Position;
            DelayTimer = 0.3F;
            Reset = false;
        }

        public override void Update()
        {
            if (DelayTimer > 0)
            {
                DelayTimer -= (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
            }
            if(DelayTimer <= 0 && Reset)
            {
                //Position.Y -= GameHandler.GraphicsHandler.ScreenSize.Y * 0.5F;
                Position.Y -= 500;
                Velocity.Y = 250F * (1 + (GameHandler.Random.Next(1, 10) / 10));
                Reset = false;
            }
            base.Update();
            Handlecolission();
        }

        public void Handlecolission()
        {
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is Raindrop)
                {
                    reset();
                }
                else
                if (entity is WallTile)
                {
                    reset();
                }
                else
                if (entity is playerCreature)
                {
                    reset();
                }
                else
                if (entity is Umbrella)
                {
                    reset();
                }
                else
                if (entity is Mother)
                {
                    reset();
                }
            }
        }

        public void reset()
        {
            Reset = true;
        }
    }
}
