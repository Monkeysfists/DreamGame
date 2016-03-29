using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Animations;
using GameLibrary.Types;
using Microsoft.Xna.Framework.Graphics;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Platforms.Chapter3
{
    public class AsteroidSpawner : TileEntity
    {
        private bool up, down;

        public AsteroidSpawner() {
            CanCollide = false;
            down = true;
            up = false;
        }

        public void Update()
        {

            if (Position.Y < 0 && up)
            {
                up = false;
                down = true;
            }
            else if (Position.Y > GameHandler.GraphicsHandler.ScreenSize.Y && down)
            {
                up = true;
                down = false;
            }

            if (up) { Position.Y++; }
            if (down) { Position.Y--; }

            if (GameHandler.Random.Next(100) == 0)
            {
                Asteroid asteroid = new Asteroid();
                asteroid.Position = this.Position;
                this.AddChild(asteroid);
                asteroid.Update();
                if (asteroid.Position.X < 0)
                {
                    RemoveChild(asteroid);
                }
            }
        }


    }
}
