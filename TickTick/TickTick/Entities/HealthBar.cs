using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TickTick.Entities
{
    public class HealthBar : TextureEntity
    {
        public HealthBar()
        {
            Position = new Vector2(0, 0);
            Texture = GameHandler.AssetHandler.GetTexture("Heart");
            Name = "Hearts";
            Visible = true;
            ResizeToTexture();
            Size *= 3;
        }
    }
}
