using Microsoft.Xna.Framework;
using System;

namespace GameLibrary {
	/// <summary>
	/// Handles game information.
	/// </summary>
	public class InfoHandler {
		/// <summary>
		/// The window title.
		/// </summary>
		public String Title {
			get {
				return _GameWindow.Title;
			}
			set {
				_GameWindow.Title = value;
			}
		}

		private GameWindow _GameWindow;

		/// <summary>
		/// Creates a new InfoHandler.
		/// </summary>
		/// <param name="gameWindow">The game window.</param>
		public InfoHandler(GameWindow gameWindow) {
			_GameWindow = gameWindow;
		}
	}
}
