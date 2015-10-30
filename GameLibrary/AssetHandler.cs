using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
//TODO: Maybe add a way to load sounds with a specific ID. Same for other assets.
namespace GameLibrary {
	/// <summary>
	/// Handles loading and retrieving of game assets.
	/// </summary>
	public class AssetHandler {
		private ContentManager _ContentManager;
		private Dictionary<string, Song> _Songs = new Dictionary<string, Song>();
		private Dictionary<string, SoundEffect> _SoundEffects = new Dictionary<string, SoundEffect>();
		private Dictionary<string, SpriteFont> _SpriteFonts = new Dictionary<string, SpriteFont>();
		private Dictionary<string, Texture2D> _Textures = new Dictionary<string, Texture2D>();

		/// <summary>
		/// Creates a new AssetManager.
		/// </summary>
		/// <param name="contentManager">The ContentManager to use.</param>
		public AssetHandler(ContentManager contentManager) {
			_ContentManager = contentManager;
		}

		/// <summary>
		/// Gets a Song. If the Song has been loaded before, that instance will be used, otherwise a new instance will be loaded.
		/// </summary>
		/// <param name="name">The name of the Song.</param>
		/// <returns>The Song.</returns>
		public Song GetSong(string name) {
			// Load if it hasn't been loaded yet
			if (!_Songs.ContainsKey(name)) {
				_Songs[name] = _ContentManager.Load<Song>(name);
			}

			return _Songs[name];
		}

		/// <summary>
		/// Gets a SoundEffect. If the SoundEffect has been loaded before, that instance will be used, otherwise a new instance will be loaded.
		/// </summary>
		/// <param name="name">The name of the SoundEffect.</param>
		/// <returns>The SoundEffect.</returns>
		public SoundEffect GetSoundEffect(string name) {
			// Load if it hasn't been loaded yet
			if (!_SoundEffects.ContainsKey(name)) {
				_SoundEffects[name] = _ContentManager.Load<SoundEffect>(name);
			}

			return _SoundEffects[name];
		}

		/// <summary>
		/// Gets a SpriteFont. If the SpriteFont has been loaded before, that instance will be used, otherwise a new instance will be loaded.
		/// </summary>
		/// <param name="name">The name of the SpriteFont.</param>
		/// <returns>The SpriteFont.</returns>
		public SpriteFont GetSpriteFont(string name) {
			// Load if it hasn't been loaded yet
			if (!_SpriteFonts.ContainsKey(name)) {
				_SpriteFonts[name] = _ContentManager.Load<SpriteFont>(name);
			}

			return _SpriteFonts[name];
		}

		/// <summary>
		/// Gets a texture. If the texture has been loaded before, that instance will be used, otherwise a new instance will be loaded.
		/// </summary>
		/// <param name="name">The name of the texture.</param>
		/// <returns>The texture.</returns>
		public Texture2D GetTexture(string name) {
			// Load if it hasn't been loaded yet
			if (!_Textures.ContainsKey(name)) {
				_Textures[name] = _ContentManager.Load<Texture2D>(name);
			}

			return _Textures[name];
		}
	}
}
