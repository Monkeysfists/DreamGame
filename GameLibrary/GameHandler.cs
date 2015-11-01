using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary {
	/// <summary>
	/// Contains all objects that allow manipulation of aspects of the game.
	/// </summary>
	public class GameHandler : Game {
		/// <summary>
		/// Used for loading and retrieving assets.
		/// </summary>
		static public AssetHandler AssetHandler;
		/// <summary>
		/// Used for playing and managing audio.
		/// </summary>
		static public AudioHandler AudioHandler;
		/// <summary>
		/// Latest GameTime.
		/// </summary>
		static public GameTime GameTime;
		/// <summary>
		/// Used for displaying and manipulating graphics.
		/// </summary>
		static public GraphicsHandler GraphicsHandler;
		/// <summary>
		/// Used for handling input.
		/// </summary>
		static public InputHandler InputHandler;
		/// <summary>
		/// Used to generate random numbers.
		/// </summary>
		static public Random Random;
		/// <summary>
		/// Used to switch between game states.
		/// </summary>
		static public StateHandler StateHandler;
		/// <summary>
		/// Used to change information about the game, such as window title.
		/// </summary>
		static public InfoHandler InfoHandler;
		/// <summary>
		/// The amount of ticks per second.
		/// </summary>
		static public int Tps {
			get {
				return _Tps;
			}
		}

		static private int _Tps;
		private DateTime _TickStart;
		private int _Ticks;

		/// <summary>
		/// Creates a new GameHandler.
		/// </summary>
		public GameHandler() {
			Content.RootDirectory = "Content";

			AssetHandler = new AssetHandler(Content);
			AudioHandler = new AudioHandler();
			GraphicsHandler = new GraphicsHandler(new GraphicsDeviceManager(this));
			InputHandler = new InputHandler();
			Random = new Random();
			StateHandler = new StateHandler();
			InfoHandler = new InfoHandler(Window);

			_TickStart = DateTime.Now;
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

			GameTime = gameTime;

			// Update input
			InputHandler.Update();

			// Check fullscreen
			if(InputHandler.OnKeyDown(Keys.F11)) {
				GraphicsHandler.FullScreen = !GraphicsHandler.FullScreen;
			}

			// Update state
			StateHandler.Update();

			// Update TPS
			if ((DateTime.Now - _TickStart).TotalMilliseconds < 1000) {
				_Ticks++;
			} else {
				_Tps = _Ticks;
				_TickStart = DateTime.Now;
				_Ticks = 0;
			}
		}
		
		protected override void Draw(GameTime gameTime) {
			base.Draw(gameTime);

			// Draw state
			GraphicsHandler.Begin();
			StateHandler.Draw();
			GraphicsHandler.End();
		}
	}
}
