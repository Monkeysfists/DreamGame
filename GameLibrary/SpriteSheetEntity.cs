using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary
{
    class SpriteSheetEntity
    {
        Texture2D Texture;
        int SheetIndex;
        int SheetColumns;
        int SheetRows;
        bool mirror;

        public SpriteSheetEntity(GameHandler gameHandler, string texture, int sheetIndex = 0)
        {
            Texture = gameHandler.AssetHandler.GetTexture(texture);//add spritesheet
            SheetIndex = sheetIndex;
            SheetColumns = 1;
            SheetRows = 1;
            /// <summary>
            /// </summary>
            /// <summary>
            /// split string to get columns and rows
            /// </summary>
            string[] assetSplit = texture.Split('@');
            if (assetSplit.Length <= 1)
                return;
            string sheetNrData = assetSplit[assetSplit.Length - 1];
            string[] columnrow = sheetNrData.Split('x');
            SheetColumns = int.Parse(columnrow[0]);
            if (columnrow.Length == 2)
                SheetRows = int.Parse(columnrow[1]);


        }

        //Draw dont know if this works no errors tho
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
        {
            int columnIndex = SheetIndex % SheetColumns;
            int rowIndex = SheetIndex / SheetColumns;
            Rectangle spritePart = new Rectangle(columnIndex * this.Width, rowIndex * this.Height, this.Width, this.Height);
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (mirror)
                spriteEffects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Texture, position, spritePart, Color.White,
                0.0f, origin, 1.0f, spriteEffects, 0.0f);
        }

        /// <summary>
        /// CopyPasta properties
        /// </summary>
        public Texture2D texture
        {
            get { return Texture; }
        }

        public Vector2 Center
        {
            get { return new Vector2(Width, Height) / 2; }
        }

        public int Width
        {
            get
            {
                return Texture.Width / SheetColumns;
            }
        }

        public int Height
        {
            get
            {
                return Texture.Height / SheetRows;
            }
        }

        public bool Mirror
        {
            get { return mirror; }
            set { mirror = value; }
        }

        public int sheetIndex
        {
            get
            {
                return SheetIndex;
            }
            set
            {
                if (value < NumberSheetElements && value >= 0)
                    SheetIndex = value;
            }
        }

        public int NumberSheetElements
        {
            get { return SheetColumns * SheetRows; }
        }
    }
}
