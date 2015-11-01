using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
//TODO: Maybe add a way to load sounds with a specific ID. Same for other assets.
namespace GameLibrary {
	/// <summary>
	/// Handles loading and retrieving of game assets.
	/// </summary>
	public class AssetHandler {
		private ContentManager _ContentManager;
		private Dictionary<String, Song> _Songs = new Dictionary<String, Song>();
		private Dictionary<String, SoundEffect> _SoundEffects = new Dictionary<String, SoundEffect>();
		private Dictionary<String, SpriteFont> _SpriteFonts = new Dictionary<String, SpriteFont>();
		private Dictionary<String, Texture2D> _Textures = new Dictionary<String, Texture2D>();
		private Dictionary<String, SpriteSheet> _SpriteSheets = new Dictionary<String, SpriteSheet>();

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
		public Song GetSong(String name) {
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
		public SoundEffect GetSoundEffect(String name) {
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
		public SpriteFont GetSpriteFont(String name) {
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
		public Texture2D GetTexture(String name) {
			// Load if it hasn't been loaded yet
			if (!_Textures.ContainsKey(name)) {
				_Textures[name] = _ContentManager.Load<Texture2D>(name);
			}

			return _Textures[name];
		}

		/// <summary>
		/// Gets a SpriteSheet. If the SpriteSheet has been loaded before, that instance will be used, otherwise a new instance will be loaded.
		/// </summary>
		/// <param name="name">The name of the SpriteSheet.</param>
		/// <returns>The SpriteSheet.</returns>
		public SpriteSheet GetSpriteSheet(String name) {
			// Load if it hasn't been loaded yet
			if (!_SpriteSheets.ContainsKey(name)) {
				_SpriteSheets[name] = new SpriteSheet(name);
			}

			return _SpriteSheets[name];
		}

		/// <summary>
		/// Gets the contents of a resource file.
		/// </summary>
		/// <param name="path">The path of the file, relative to the game.</param>
		/// <returns>The file's contents.</returns>
		public String ReadFile(String path) {
			StreamReader reader = new StreamReader(_ContentManager.RootDirectory + "/" + path);
			string result = reader.ReadToEnd();
			reader.Close();

			return result;
		}

		/// <summary>
		/// Writes text to a file.
		/// </summary>
		/// <param name="path">The path of the file, relative to the game.</param>
		/// <param name="text">The contents to write.</param>
		/// <param name="append">Whether to append to the end or overwrite.</param>
		public void WriteFile(String path, String text, bool append) {
			StreamWriter writer = new StreamWriter(_ContentManager.RootDirectory + "/" + path, append);
			writer.Write(text);
			writer.Close();
		}

		/// <summary>
		/// Gets the amount of files in the specified content directory.
		/// </summary>
		/// <param name="path">The path of the directory.</param>
		/// <returns>The amount of files.</returns>
		public int CountFiles(String path) {
			return Directory.GetFiles(@_ContentManager.RootDirectory + "/" + path).Length;
        }
	}
}
