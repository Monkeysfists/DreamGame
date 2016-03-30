using GameLibrary;
using System;
using Microsoft.Xna.Framework;
using TickTick.Entities.Tiles.Creatures;
using System.Collections.Generic;
using GameLibrary.Types;
using TickTick.Entities.Tiles.Platforms.Chapter1;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Platforms
{
    public class BlockBoat : PlatformTile
    {
        Paperboat paperboat;
        public Vector2 StarterPosition;
        private float _PreviousY;
        private bool _OnGround;

        public bool Startbool { get; private set; }

        public BlockBoat()
        {
            paperboat = new Paperboat();
            AddChild(paperboat);
            //Returns a random block sprite
            //Texture = GameHandler.AssetHandler.GetTexture("chapter1//blokken//blok" + GameHandler.Random.Next(1,27));
            StarterPosition = Position;
            //Position = paperboat.Position - new Vector2(100,0);
            Size = paperboat.Size;

            paperboat.Position.Y -= 20;
            paperboat.Velocity.X = 0;
            Size.Y -= 70;
            Origin.X += 60;
        }

        public override void Update()
        {
            paperboat.Active = false;
            Handlecolission();
            base.Update();
            if (Startbool)
            {
                Velocity.X = 200F;
            }
            Console.WriteLine(paperboat.Velocity.ToString());
        }

        public void Handlecolission()
        {
            _OnGround = false;
            Console.WriteLine(paperboat.Velocity.ToString());

            Position = new Vector2((float)Math.Floor(Position.X), (float)Math.Floor(Position.Y));

            // Handle collisions
            foreach (Entity entity in GetCollidingEntities(new List<Entity>(Parent.Children), Vector2.Zero, Vector2.Zero))
            {
                if (entity is PlayerCreature || entity is Mother)
                {
                    Startbool = true;
                }
                else
                if (entity is Paperboat)
                {
                    entity.Velocity.X = 0;
                }
                else
                if(entity is PlatformTile || entity is WallTile)
                {
                    Active = false;
                }else
                if (!(entity is PlayerCreature))
                {
                    RectangleF playerBounds = GlobalCollisionBox;
                    RectangleF tileBounds = entity.GlobalCollisionBox;
                    playerBounds.Height++;
                    Vector2 depth = CalculateIntersectionDepth(playerBounds, tileBounds);

                    if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                    {
                        if (!(entity is WaterTile) || !(entity is Raindrop))
                        {
                            Position.X += depth.X;
                        }
                    }
                    else
                    {
                        if (_PreviousY - 1 <= tileBounds.Top && Velocity.Y >= 0)
                        {
                            _OnGround = true;

                            Velocity.Y = 0F;
                        }
                        if (!(entity is WaterTile) || (entity is WaterTile && _OnGround))
                        {
                            Position.Y += depth.Y + 2F;
                            Velocity.Y = 0F;
                        }
                        else if (entity is WaterTile && !_OnGround && Velocity.Y > 0F)
                        {
                            Position.Y -= depth.Y - 1;
                        }
                    }
                }


            }

            _PreviousY = GlobalCollisionBox.Bottom;
        }


        public Vector2 CalculateIntersectionDepth(Rectangle rectangle1, Rectangle rectangle2)
        {
            Vector2 center1 = new Vector2(rectangle1.Center.X, rectangle1.Center.Y);
            Vector2 center2 = new Vector2(rectangle2.Center.X, rectangle2.Center.Y);
            Vector2 minDistance = new Vector2(rectangle1.Width + rectangle2.Width, rectangle1.Height + rectangle2.Height) / 2;
            Vector2 distance = center1 - center2;
            Vector2 depth = Vector2.Zero;
            if (distance.X > 0)
            {
                depth.X = minDistance.X - distance.X;
            }
            else
            {
                depth.X = -minDistance.X - distance.X;
            }
            if (distance.Y > 0)
            {
                depth.Y = minDistance.Y - distance.Y;
            }
            else
            {
                depth.Y = -minDistance.Y - distance.Y;
            }
            return depth;
        }
    }
}
