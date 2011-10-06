using System.Collections.Generic;

namespace Paraiba.Collections.Generic {
	public static class DictionaryExtensions {
		public static TValue GetValueOrDefault<TKey, TValue>(
				this IDictionary<TKey, TValue> dict, TKey key) {
			TValue value;
			if (dict.TryGetValue(key, out value))
				return value;
			return default(TValue);
		}

		public static int Increment<TKey>(this IDictionary<TKey, int> dict, TKey key) {
			return dict.Increment(key, 1);
		}

		public static int Increment<TKey>(this IDictionary<TKey, int> dict, TKey key,
		                                  int increment) {
			int value;
			if (dict.TryGetValue(key, out value)) {
				dict[key] = value + increment;
				return value + increment;
			}
			dict.Add(key, increment);
			return increment;
		}

		public static int Decrement<TKey>(this IDictionary<TKey, int> dict, TKey key) {
			return dict.Increment(key, -1);
		}

		public static int Decrement<TKey>(this IDictionary<TKey, int> dict, TKey key,
		                                  int decrement) {
			return dict.Increment(key, -decrement);
		}
	}
}