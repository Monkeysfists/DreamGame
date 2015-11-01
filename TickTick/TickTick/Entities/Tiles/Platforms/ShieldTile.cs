using Microsoft.Xna.Framework;
using System;
using GameLibrary;

namespace TickTick.Entities.Tiles
{
    /// <summary>
    /// A tile which can be picked up an provides a shield.
    /// </summary>
    class ShieldTile : TileEntity
    {

        /// <summary>
		/// Creates a new ShieldTile.
		/// </summary>
		public ShieldTile()
        {
            Texture = GameHandler.AssetHandler.GetTexture("Tiles/spr_shield");
            Origin = new Vector2(-((Size.X - Texture.Width) / 2), 10);
            ResizeToTexture();
        }
    }
}
