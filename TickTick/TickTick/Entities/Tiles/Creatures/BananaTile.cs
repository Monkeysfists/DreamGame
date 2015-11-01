using Microsoft.Xna.Framework;
using System;
using GameLibrary;

namespace TickTick.Entities.Tiles
{
    /// <summary>
    /// A Banana peel, which can be picked up.
    /// </summary>
    class BananaTile : TileEntity
{
    /// <summary>
    /// Creates a new BananTile.
    /// </summary>
    public BananaTile()
    {
        Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_bananaPeel");
        Origin = new Vector2(-((Size.X - Texture.Width) / 2), 10);
        ResizeToTexture();
    }

    public override void Update()
    {
        base.Update();

        // Make the banana peel bounce
        double x = GameHandler.GameTime.TotalGameTime.TotalSeconds * 3.0F + Position.X;
        Position.Y += (float)Math.Sin(x) * 0.2f;
    }
}
}
