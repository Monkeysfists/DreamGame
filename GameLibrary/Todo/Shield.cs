using Microsoft.Xna.Framework;
using System;

    class Shield : SpriteGameObject
    {
        protected float bounce;

        public Shield(int layer = 0, string id = "") : base("Sprites/spr_water", layer, id) 
        {
        }

        public override void Update(GameTime gameTime)
        {
            double t = gameTime.TotalGameTime.TotalSeconds * 3.0f + Position.X;
            bounce = (float)Math.Sin(t) * 0.2f;
            position.Y += bounce;
            Player player = GameWorld.Find("player") as Player;
            if (this.visible && this.CollidesWith(player))
            {
                this.visible = false;
                player.InvincibleTimer = 5;
            }
        }
    }

