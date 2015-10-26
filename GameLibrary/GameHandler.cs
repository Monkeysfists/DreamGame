using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary {
	/// <summary>
	/// Contains all objects that allow manipulation of aspects of the game.
	/// </summary>
	public class GameHandler : Game, Updatable, Drawable {
		public AssetHandler AssetHandler;
		public AudioHandler AudioHandler;
		public GraphicsHandler GraphicsHandler;
		public InputHandler InputHandler;
		public Random Random;
		public Entity Scene;
		public SpriteBatch SpriteBatch;

		public GameHandler() {
			Content.RootDirectory = "Content";

			AssetHandler = new AssetHandler(Content);
			AudioHandler = new AudioHandler();
			GraphicsHandler = new GraphicsHandler(GraphicsDevice, new GraphicsDeviceManager(this));
			InputHandler = new InputHandler();
			Random = new Random();
		}

		/// <summary>
		/// Preload Songs, SoundEffects, Textures and other game objects.
		/// </summary>
		protected override void LoadContent() {
			base.LoadContent();
		}

		protected override void Update(GameTime gameTime) {
			base.Update(gameTime);

			// Update input
			InputHandler.Update();

			// Handle input
			Scene.HandleInput();

			// Update
			Scene.Update();
		}

		public void Update() {

		}

		protected override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			// Clear screen
			GraphicsDevice.Clear(Color.Black);

			// Draw game
			SpriteBatch.Begin();
			Scene.Draw();
			SpriteBatch.End();
		}
	}
}
