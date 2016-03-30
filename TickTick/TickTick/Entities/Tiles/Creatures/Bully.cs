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

namespace TickTick.Entities.Tiles.Creatures
{
    public class Bully : CreatureTileEntity
    {
        private Animation BullyIdleAnimation;
        private Animation BullyRunAnimation;
        private Animation BullyAttackAnimation;

        private float Health;
        private float Speed;

        private Vector2 PlayerPosition;

        private float IntervalTimer;
        private bool canAttack;

        private bool mirrored;

        List<Entity> EntityList;
        playerCreature player;


        public Bully()
        {
            Health = 100;
            Name = "Bully";

            // Speed
            Speed = 200F;

            //Timer
            IntervalTimer = 1F;

            //Animations
            BullyIdleAnimation = new BullyIdleAnimation();
            BullyRunAnimation = new BullyRunAnimation();
            BullyAttackAnimation = new BullyAttackAnimation();
            Animation = BullyIdleAnimation;
            GetPlayer();

            // Size
            Size = Animation.SpriteSheet.CellSize * LevelEntity.TileSize / 10;
            Origin.X = (Size.X - 72) / 2;
            Origin.Y = (Size.Y - 55) / 2;

        }

        public override void Update()
        {
            // Animation directions
            if (Velocity.X > 0)
            {
                IdleAnimation.FlipHorizontally = false;
            }
            else if (Velocity.X < 0)
            {
                IdleAnimation.FlipHorizontally = true;
            }

            if (IntervalTimer > 0)
            {
                canAttack = false;
            }
            else if (IntervalTimer <= 0)
            {
                canAttack = true;
            }

            HandleCollision();
            
            /*if (player.Position.X > Position.X)
                mirrored = true;
            if (IntervalTimer == 0 && Visible)
                GetPlayer();
            if (!mirrored && Position.X - player.Position.X > 20 && IntervalTimer == 0)
                Position.X -= Speed;
            else if (mirrored && player.Position.X - Position.X > 20 && IntervalTimer == 0)
                Position.X += Speed;

            if(MathHelper.Distance(player.Position.X, Position.X) < 20)
            {
                Animation = BullyAttackAnimation;
                IntervalTimer += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
            }

            if (mirrored)
            {
                Animation.FlipHorizontally = true;
            }

            if (IntervalTimer > 2)
                IntervalTimer = 0;
                
            base.Update();
            Size = Animation.SpriteSheet.CellSize * LevelEntity.TileSize / 10;*/
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void HandleCollision()
        {

        }

        public bool CanAttack
        {
            get { return canAttack; }
        }

        public float GetHealth
        {
            get { return Health; }
        }
        private void GetPlayer()
        {
            EntityList = FindChildrenByName("player", true);
            if (EntityList.Count > 0)
                player = (playerCreature)EntityList[0];
        }

        public void Attack()
        {
            //if()
        }

    }
}
