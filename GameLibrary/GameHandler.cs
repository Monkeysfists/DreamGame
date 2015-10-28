using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary {
	/// <summary>
	/// Contains all objects that allow manipulation of aspects of the game.
	/// </summary>
	public class GameHandler : Game, Updatable, Drawable {
		static public AssetHandler AssetHandler;
		static public AudioHandler AudioHandler;
		static public GraphicsHandler GraphicsHandler;
		static public InputHandler InputHandler;
		static public Random Random;
		static public Entity Scene;

		public GameHandler() {
			Content.RootDirectory = "Content";

			AssetHandler = new AssetHandler(Content);
			AudioHandler = new AudioHandler();
			GraphicsHandler = new GraphicsHandler(new GraphicsDeviceManager(this));
			InputHandler = new InputHandler();
			Random = new Random();
		}

		/// <summary>
		/// Initialize the game.
		/// </summary>
		protected override void Initialize() {
			base.Initialize();
		}

		/// <summary>
		/// Preload Songs, SoundEffects, Textures and other game objects.
		/// </summary>
		protected override void LoadContent() {
			base.LoadContent();

			GraphicsHandler.LoadContent();
		}

		protected override void Update(GameTime gameTime) {
			base.Update(gameTime);

			Update();
		}

		public void Update() {
			// Update input
			InputHandler.Update();

			// Check if a scene is set
			if(Scene != null) {
				// Handle input
				Scene.HandleInput();

				// Update
				Scene.Update();
			}
		}

		protected override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			Draw();
		}

		public void Draw() {
			// Clear screen
			GraphicsHandler.Clear(Color.Black);

			// Draw game
			// Check if a scene is set
			if (Scene != null) {
				GraphicsHandler.SpriteBatch.Begin();
				Scene.Draw();
				GraphicsHandler.SpriteBatch.End();
			}
		}
	}
}
