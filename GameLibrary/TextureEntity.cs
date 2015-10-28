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
        public SpriteSheetEntity sprite;

        public TextureEntity(GameHandler gameHandler, string id, string texturename, int sheetIndex) : base(gameHandler)
        {
            if (texturename != "")
                sprite = new SpriteSheetEntity(gameHandler, texturename, sheetIndex);
            else
                sprite = null;
            Size = new Vector2(sprite.Width, sprite.Height);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, Width, Height), Tint * Opacity);
        }
        

        public Vector2 Center
        {
            get { return new Vector2(Width, Height) / 2; }
        }

        public int Width
        {
            get
            {
                return sprite.Width;
            }
        }

        public int Height
        {
            get
            {
                return sprite.Height;
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
