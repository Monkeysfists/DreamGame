using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    public class Tunnel : PlatformTile
    {
        public NoCollisionObject tunnelfront, tunnelback;

        public Tunnel()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/tunnel_boven");
            tunnelback = new NoCollisionObject("chapter1/tunnel_achter", new Vector2(100,600));
            tunnelfront = new NoCollisionObject("chapter1/tunnel_voor", new Vector2(100, 600));
            tunnelback.Layer = 500;
            tunnelfront.Layer = 1;
            tunnelback.Position = this.Position;
            tunnelfront.Position = this.Position;
            this.AddChild(tunnelfront);
            this.AddChild(tunnelback);
            //TODO: collision met player
        }
    }
}
