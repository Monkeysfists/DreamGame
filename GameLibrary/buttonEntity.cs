using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary
{
    class buttonEntity : TextureEntity
    {
        protected bool pressed;

        public buttonEntity(GameHandler gameHandler, string id, Vector2 position, Texture2D texture)
        : base(gameHandler, id, position, texture)
    {
            pressed = false;
        }

        public override void HandleInput(InputHandler inputHandler)
        {
            pressed = inputHandler.OnMouseButtonDown(InputHandler.MouseButton.Left) &&
                BoundingBox.Contains((int)inputHandler.MousePosition.X, (int)inputHandler.MousePosition.Y);
        }


        public bool Pressed
        {
            get { return pressed; }
        }
    }
}

