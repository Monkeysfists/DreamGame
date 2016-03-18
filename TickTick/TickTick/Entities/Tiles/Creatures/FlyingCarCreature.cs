using GameLibrary;
using GameLibrary.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TickTick.Animations;
using TickTick.Entities.States;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Entities.Tiles.Walls;

namespace TickTick.Entities.Tiles.Creatures
{
    class FlyingCarCreature : CreatureTileEntity
    {
        public int Health
        {
            get
            {
                return _Health;
            }
            set
            {
                if (value < Health)
                {
                    if (Position.Y > Parent.Size.Y)
                    {
                        GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_fall"));
                    }
                    else
                    {
                        GameHandler.AudioHandler.PlaySoundEffect(GameHandler.AssetHandler.GetSoundEffect("Sounds/snd_player_die"));
                    }
                }

                _Health = value;

                if (Health <= 0)
                {
                    if (Position.Y <= Parent.Size.Y)
                    {
                        Velocity.Y = -900;
                    }

                    Animation = DieAnimation;
                    ((PlayingState)Parent.Parent).GameOver = true;
                }
            }


        }
        /// <summary>
		/// The keys to move left.
		/// </summary>
		public List<Keys> LeftKey;
        /// <summary>
        /// The keys to move right.
        /// </summary>
        public List<Keys> RightKey;
        /// <summary>
        /// The right movement speed.
        /// </summary>
        public float RightSpeed;
        /// <summary>
        /// The animation to play when moving right.
        /// </summary>
        public Animation RightAnimation;
        /// <summary>
        /// The animation to play when exploding.
        /// </summary>
        public Animation ExplodeAnimation;
        /// <summary>
        /// Invincible timer control bool
        /// </summary>
        private bool Invin;


        private int _Health;
        private bool _Won;
        private float InvinceTimer;
    }
}
