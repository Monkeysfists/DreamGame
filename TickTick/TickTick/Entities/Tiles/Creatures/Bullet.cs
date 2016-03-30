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
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles
{
    public class Bullet : TextureEntity
    {
        bool right;



        public Bullet(bool right)
        {
            this.right = right;
            Texture = GameHandler.AssetHandler.GetTexture("chapter3/bullet");
            CanCollide = true;
        }

        public override void Update()
        {            
            if (right)
                Velocity.X = 1000F;
            else if (!right)
                Velocity.X = -1000F;   
            foreach (Entity entity in GetCollidingEntities(new List<Entity>((Parent).Parent.Children), Vector2.Zero, Vector2.Zero))
            {
            if(entity is TeddyBear)
                this.Visible = false;
                Parent.RemoveChild(this);
                entity.Active = false;
                RemoveChild(entity);
            }
            base.Update();
        }
    }
}
