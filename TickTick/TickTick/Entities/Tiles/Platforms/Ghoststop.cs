using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    class GhostStop : PlatformTile
    {
        public GhostStop()
        {
            Texture = GameHandler.AssetHandler.GetTexture("grond");
            Size = new Microsoft.Xna.Framework.Vector2(Texture.Width, Texture.Height);
            Visible = false;
            CanCollide = false;
        }

    }
}
