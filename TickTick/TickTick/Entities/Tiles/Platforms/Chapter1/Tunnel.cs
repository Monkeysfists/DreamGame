using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms.Chapter1
{
    public class Tunnel : PlatformTile
    {
        public NoCollisionObject tunnelfront, tunnelback;

        public Tunnel()
        {
            Texture = GameHandler.AssetHandler.GetTexture("chapter1/tunnel_boven");
            tunnelback = new NoCollisionObject("chapter1/tunnel_achter");
            tunnelfront = new NoCollisionObject("chapter1/tunnel_voor");
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
