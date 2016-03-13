using GameLibrary;

namespace TickTick.Entities.Tiles.Platforms
{
    /// <summary>
    /// A default platform.
    /// </summary>
    public class GoalTile : PlatformTile
    {
        /// <summary>
        /// Creates a new DefaultPlatform.
        /// </summary>
        public GoalTile()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("");
        }
    }
}
