﻿using GameLibrary;
using System;

namespace TickTick.Animations
{
    /// <summary>
    /// A moving player.
    /// </summary>
    public class TeddyAttackAnimation : Animation
    {
        /// <summary>
        /// Creates a new PlayerMoveAnimation.
        /// </summary>
        public TeddyAttackAnimation()
        {
            SpriteSheet = GameHandler.AssetHandler.GetSpriteSheet("");
            //FrameTime = TimeSpan.FromMilliseconds(50);
        }
    }
}