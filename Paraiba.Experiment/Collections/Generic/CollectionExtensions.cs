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

namespace Paraiba.Collections.Generic {
	public static class CollectionExtensions {
		/// <summary>
		///   指定したシーケンスの要素をコレクションに追加します。
		/// </summary>
		/// <typeparam name="T"> シーケンスの要素の型 </typeparam>
		/// <param name="collection"> 追加するコレクション </param>
		/// <param name="source"> 追加される要素のシーケンス </param>
		/// <returns> コレクション </returns>
		public static void AddRange<T>(
				this ICollection<T> collection, IEnumerable<T> source) {
			foreach (var item in source) {
				collection.Add(item);
			}
		}

		/// <summary>
		///   コレクションに指定した要素を追加して、有効要素数を拡張する
		/// </summary>
		/// <param name="collection"> 拡張するコレクション </param>
		/// <param name="count"> 拡張するコレクションのサイズ </param>
		public static ICollection<T> Extend<T>(
				this ICollection<T> collection, int count) {
			return Extend(collection, count, default(T));
		}

		/// <summary>
		///   コレクションに指定した要素を追加して、有効要素数を拡張する
		/// </summary>
		/// <param name="collection"> 拡張するコレクション </param>
		/// <param name="count"> 拡張するコレクションのサイズ </param>
		/// <param name="defaultElement"> 拡張する際に追加する要素 </param>
		public static ICollection<T> Extend<T>(
				this ICollection<T> collection, int count, T defaultElement) {
			for (int i = collection.Count; i < count; i++) {
				collection.Add(defaultElement);
			}
			return collection;
		}

		/// <summary>
		///   コレクションに指定した要素を追加して、有効要素数を拡張する
		/// </summary>
		/// <param name="collection"> 拡張するコレクション </param>
		/// <param name="count"> 拡張するコレクションのサイズ </param>
		/// <param name="defaultElementFunc"> 拡張する際に追加する要素を取得するデリゲート </param>
		public static ICollection<T> Extend<T>(
				this ICollection<T> collection, int count,
				Func<T> defaultElementFunc) {
			for (int i = collection.Count; i < count; i++) {
				collection.Add(defaultElementFunc());
			}
			return collection;
		}

		public static TResult[] SelectToArray<TSource, TResult>(
				this ICollection<TSource> collection,
				Func<TSource, TResult> func) {
			var array = new TResult[collection.Count];
			var i = 0;
			foreach (var item in collection) {
				array[i++] = func(item);
			}
			return array;
		}

		public static TResult[] SelectToArray<TSource, TResult>(
				this ICollection<TSource> collection,
				Func<TSource, int, TResult> func) {
			var array = new TResult[collection.Count];
			var i = 0;
			foreach (var item in collection) {
				array[i] = func(item, i++);
			}
			return array;
		}

		public static List<TResult> SelectToList<TSource, TResult>(
				this ICollection<TSource> collection,
				Func<TSource, TResult> func) {
			var list = new List<TResult>(collection.Count);
			foreach (var item in collection) {
				list.Add(func(item));
			}
			return list;
		}

		public static List<TResult> SelectToList<TSource, TResult>(
				this ICollection<TSource> collection,
				Func<TSource, int, TResult> func) {
			var list = new List<TResult>(collection.Count);
			var i = 0;
			foreach (var item in collection) {
				list.Add(func(item, i++));
			}
			return list;
		}
	}
}