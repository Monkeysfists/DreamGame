using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameLibrary {
	/// <summary>
	/// Handles audio-related operations.
	/// </summary>
	public class AudioHandler {
		/// <summary>
		/// The volume of Songs.
		/// </summary>
		public float SongVolume {
			get {
				return MediaPlayer.Volume;
			}
			set {
				MediaPlayer.Volume = value;
			}
		}
		/// <summary>
		/// The volume of SoundEffects.
		/// </summary>
		public float SoundEffectVolume {
			get {
				return SoundEffect.MasterVolume;
			}
			set {
				SoundEffect.MasterVolume = value;
			}
		}

		/// <summary>
		/// Plays a Song.
		/// </summary>
		/// <param name="song">The Song to play.</param>
		/// <param name="repeat">Whether the Song should repeat.</param>
		public void PlaySong(Song song, bool repeat) {
			MediaPlayer.IsRepeating = repeat;
			MediaPlayer.Play(song);
		}

		/// <summary>
		/// Plays a SoundEffect.
		/// </summary>
		/// <param name="soundEffect">The SoundEffect to play.</param>
		public void PlaySoundEffect(SoundEffect soundEffect) {
			soundEffect.Play();
		}
	}
}
