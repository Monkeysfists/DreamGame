using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using TickTick.Entities.Tiles.Platforms;
using TickTick.Animations;
using GameLibrary.Types;
using Microsoft.Xna.Framework.Graphics;
using TickTick.Entities.Tiles.Creatures;

namespace TickTick.Entities.Tiles.Platforms
{
    public class PCDesk : PlatformTile
    {
        private List<Entity> EntityList;
        private PlayerCreature player;
        private Vector2 PlayerPosition;
        private float UpdateTimer;

        public PCDesk()
        {
            //TODO
            Texture = GameHandler.AssetHandler.GetTexture("chapter3/computer");
            UpdateTimer = 0;
        }

        public override void Update()
        {
            if(UpdateTimer > 1)
            {
                UpdateTimer += (float)GameHandler.GameTime.ElapsedGameTime.TotalSeconds;
                UpdateTimer = 0;
                GetPlayerPos();
            }

            if(player != null)
                if(MathHelper.Distance(PlayerPosition.X, Position.X) < 20)
                    //play computer transfer animation
            base.Update();
        }

        private void GetPlayerPos()
        {
            EntityList = FindChildrenByName("player", true);
            player = (PlayerCreature)EntityList[0];
            PlayerPosition = player.Position;

        }
    }
}
