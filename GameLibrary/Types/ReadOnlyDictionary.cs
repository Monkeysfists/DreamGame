using System.Collections.Generic;

namespace GameLibrar.Types {
	public class ReadOnlyDictionary<TKey, TValue> {
		/// <summary>
		/// The amount of items in the dictionary.
		/// </summary>
		public int Count {
			get {
				return _Dictionary.Count;
			}
		}
		/// <summary>
		/// The keys in the dictionary.
		/// </summary>
		public ICollection<TKey> Keys {
			get {
				return _Dictionary.Keys;
			}
		}
		/// <summary>
		/// The values in the dictionary.
		/// </summary>
		public ICollection<TValue> Values {
			get {
				return _Dictionary.Values;
			}
		}
		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <param name="key">The key to get.</param>
		/// <returns>The value associated with the specified key.</returns>
		public TValue this[TKey key] {
			get {
				return _Dictionary[key];
			}
		}

		private readonly IDictionary<TKey, TValue> _Dictionary;

		/// <summary>
		/// Creates a new ReadOnlyDictionary from the specified dictionary.
		/// </summary>
		/// <param name="dictionary">The dictionary to use.</param>
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary) {
			_Dictionary = dictionary;
		}

		/// <summary>
		/// Checks if the dictionary contains the specified key-value pair.
		/// </summary>
		/// <param name="item">The key-value pair to check.</param>
		/// <returns>Whether the dictionary contains the specified key-value pair.</returns>
		public bool Contains(KeyValuePair<TKey, TValue> item) {
			return _Dictionary.Contains(item);
		}

		/// <summary>
		/// Checks if the dictionary contains the specified key.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>Whether the dictionary contains the specified key.</returns>
		public bool ContainsKey(TKey key) {
			return _Dictionary.ContainsKey(key);
		}
	}
}
