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

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Paraiba.Utility {
	public interface SetIndexer<TKey, TValue> {
		TValue this[TKey index] { set; }
	}

	public interface GetIndexer<TKey, TValue> {
		TValue this[TKey index] { set; }
	}

	public interface Indexer<TKey, TValue>
			: SetIndexer<TKey, TValue>, GetIndexer<TKey, TValue> {}

	/// <summary>
	///   IList <TValue>を Indexer
	///           <TKey, TValue>に適用するためのアダプターを表します。
	/// </summary>
	/// <typeparam name="TValue"> </typeparam>
	public struct ListIndexer<TKey, TValue> : Indexer<TKey, TValue> {
		private readonly IList<TValue> _array;
		private readonly Func<TKey, int> _toIntFunc;

		public ListIndexer(IList<TValue> array)
				: this(array, key => Convert.ToInt32(key)) {}

		public ListIndexer(IList<TValue> array, Func<TKey, int> toIntFunc) {
			Debug.Assert(array != null);
			Debug.Assert(toIntFunc != null);
			_array = array;
			_toIntFunc = toIntFunc;
		}

		#region Indexer<TKey,TValue> Members

		public TValue this[TKey index] {
			get { return _array[_toIntFunc(index)]; }
			set { _array[_toIntFunc(index)] = value; }
		}

		#endregion
	}

	/// <summary>
	///   IList <TValue>を Indexer
	///           <int, TValue>に適用するためのアダプターを表します。
	/// </summary>
	/// <typeparam name="TValue"> </typeparam>
	public struct ListIndexer<TValue> : Indexer<int, TValue> {
		private readonly IList<TValue> _array;

		public ListIndexer(IList<TValue> array) {
			_array = array;
		}

		#region Indexer<int,TValue> Members

		public TValue this[int index] {
			get { return _array[index]; }
			set { _array[index] = value; }
		}

		#endregion
	}
}