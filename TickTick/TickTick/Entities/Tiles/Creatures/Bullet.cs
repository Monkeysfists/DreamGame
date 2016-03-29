using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.States;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;
using TickTick.Entities.Tiles.Platforms.Chapter1;
using TickTick.Entities.Tiles.Platforms.Chapter3;

namespace TickTick.Entities.Tiles.Creatures
{
    class Bullet : CreatureTileEntity
    {
        bool right;

        public Bullet(bool right)
        {
            this.right = right;
            Texture = GameHandler.AssetHandler.GetTexture("chapter3/bullet");
        }

        public override void Update()
        {            
            base.Update();

            if (right)
                Position.X += 2;
            else
                Position.X -= 2;

            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                this.Visible = false;
                Parent.RemoveChild(this);
            }
        }
    }
}
