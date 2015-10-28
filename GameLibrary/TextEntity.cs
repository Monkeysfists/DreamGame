using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary
{
    class TextEntity : Entity
    {
        public string Text;
        public SpriteFont Font;
        Color color;

        public TextEntity(GameHandler gameHandler, string id, Vector2 position, string text) : base(gameHandler)
        {
            Font = gameHandler.AssetHandler.GetSpriteFont("Arial");
            Text = text;
        }
        //Fix draw
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
			spriteBatch.DrawString(Font, Text, GlobalPosition, Color);
        }
        
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public string text
        {
            get { return Text; }
            set { Text = value; }
        }
        public new Vector2 Size
        {
            get
            {
                return Font.MeasureString(Text);
            }
        }
    }
}
