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
        public GoalTile(int level)
        {
            if(level != 6 && level != 7)
                Texture = GameHandler.AssetHandler.GetTexture("stoep");
            //TODO
            if (level == 6 || level == 7)
            {
                Size.Y = 1000;
            }
        }
    }
}
