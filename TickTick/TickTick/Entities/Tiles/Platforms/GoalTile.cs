using GameLibrary;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Platforms
{
    /// <summary>
    /// A default platform.
    /// </summary>
    public class GoalTile : WallTile
    {
        /// <summary>
        /// Creates a new DefaultPlatform.
        /// </summary>
        public GoalTile()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("finish_flag");
            Size = new Microsoft.Xna.Framework.Vector2(LevelEntity.TileSize*2,LevelEntity.TileSize*2);
        }
    }
}
