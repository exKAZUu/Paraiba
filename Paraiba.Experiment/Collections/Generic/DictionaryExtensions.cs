#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Collections.Generic;

namespace Paraiba.Collections.Generic {
	public static class DictionaryExtensions {
		public static TValue GetValueOrDefault<TKey, TValue>(
				this IDictionary<TKey, TValue> dict, TKey key) {
			TValue value;
			if (dict.TryGetValue(key, out value)) {
				return value;
			}
			return default(TValue);
		}

		public static int Increment<TKey>(
				this IDictionary<TKey, int> dict, TKey key) {
			return dict.Increment(key, 1);
		}

		public static int Increment<TKey>(
				this IDictionary<TKey, int> dict, TKey key,
				int increment) {
			int value;
			if (dict.TryGetValue(key, out value)) {
				dict[key] = value + increment;
				return value + increment;
			}
			dict.Add(key, increment);
			return increment;
		}

		public static int Decrement<TKey>(
				this IDictionary<TKey, int> dict, TKey key) {
			return dict.Increment(key, -1);
		}

		public static int Decrement<TKey>(
				this IDictionary<TKey, int> dict, TKey key,
				int decrement) {
			return dict.Increment(key, -decrement);
		}
	}
}