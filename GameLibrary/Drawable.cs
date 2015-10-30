namespace GameLibrary {
	/// <summary>
	/// Represents a game object that is drawable.
	/// </summary>
	interface Drawable {
		/// <summary>
		/// Performs per-frame draws. Ideally executed 60 times per second, but will drop frames if the computer is running slowly.
		/// </summary>
		void Draw();
	}
}
