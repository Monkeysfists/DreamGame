using GameLibrary;
using Microsoft.Xna.Framework;

namespace TickTick.Entities.States
{
    class FadeState : State
    {
        private float _Timer, _Fader;
        private TextureEntity _Black;

        public FadeState() : base("fade") {
            _Black = new TextureEntity();
            _Black.Texture = GameHandler.AssetHandler.GetTexture("Backgrounds/spr_sky");
            AddChild(_Black);
            Size = new Vector2(GameHandler.GraphicsHandler.Resolution.X, GameHandler.GraphicsHandler.Resolution.Y);
            _Black.Size = Size;
        }
        /*
        public override void Update(GameTime gameTime)
        {
            base.Update();
            _Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _Fader += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_Timer > 1)
            {
                GameHandler.StateHandler.CurrentState = GameHandler.StateHandler.States["playing"];
                //fade in toevoegen
            }

        }
        */

        public override void Draw()
        {
            base.Draw();
            //Geen idee of dit goed is
            GameHandler.GraphicsHandler.DrawSquare(Vector2.Zero, new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth,GraphicsDeviceManager.DefaultBackBufferHeight), 0, Color.Black * _Fader, Color.Black * _Fader);
        }
    }
}
