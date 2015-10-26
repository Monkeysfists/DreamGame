using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary
{
    class TextureEntity : Entity
    {
        public float Opacity = 1F;

        public TextureEntity(GameHandler gameHandler, string id, Vector2 position, Texture2D texture) : base(gameHandler)
        {
            Texture = texture;
            Size = new Vector2(texture.Width, texture.Height);
        }
        //Fix draw
        /*
        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Draw(Texture, new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, (int)GlobalSize.X, (int)GlobalSize.Y), Tint * Opacity);
        }
        */

        public Vector2 Center
        {
            get { return new Vector2(Width, Height) / 2; }
        }

        public int Width
        {
            get
            {
                return Texture.Width;
            }
        }

        public int Height
        {
            get
            {
                return Texture.Height;
            }
        }
        
        public Rectangle BoundingBox
        {
            get
            {
                int x = (int)GlobalPosition.X;
                int y = (int)GlobalPosition.Y;
                return new Rectangle(x, y, Width, Height);
            }
        }

    }
}
