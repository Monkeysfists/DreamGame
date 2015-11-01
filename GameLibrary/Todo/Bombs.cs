using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TickTick.gameobjects
{
    class Bombs : AnimatedGameObject
    {
        int Timer;

        Bombs(Vector2 position, bool Mirror)
        {
            this.position = position;
            this.LoadAnimation("Sprites/Player/spr_idle","idle",false);
            this.LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f);
            this.position = position;
            this.Mirror = Mirror;
            velocity = Vector2.Zero;
            Timer = 200;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //add key
            Visible = true;
            Timer--;
            PlayAnimation("idle");
            this.velocity.Y = -0.02F;
            if (Mirror)
            {
                this.velocity.X = -0.02F;
            }
            else
            {
                this.velocity.X = 0.02F;
            }

            Position = Position - velocity;

            CheckCollision();
        }

        public void CheckCollision()
        {
            //TODO add colding function
            if ( && velocity.Y <= 0.00F || Timer <= 0)
            {
                
                PlayAnimation("explode");
                if (Current.AnimationEnded)
                {
                    Visible = false;
                    Timer = 200;
                    Position = base.Position;
                }
            }
        }

    }
}
