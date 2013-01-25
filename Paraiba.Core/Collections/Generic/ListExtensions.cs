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
	public static class ListExtensions {
		/// <summary>
		/// Extends the list to the specified size adding the default value.
		/// </summary>
		/// <typeparam name="T">The type of the list element</typeparam>
		/// <param name="list">The list to be extended</param>
		/// <param name="count">The size to extend</param>
		/// <returns>The extended list</returns>
		public static IList<T> Extend<T>(this IList<T> list, int count) {
			return Extend(list, count, default(T));
		}

		/// <summary>
		/// Extends the list to the specified size adding the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the list element</typeparam>
		/// <param name="list">The list to be extended</param>
		/// <param name="count">The size to extend</param>
		/// <param name="defaultValue">The value to be added</param>
		/// <returns>The extended list</returns>
		public static IList<T> Extend<T>(
				this IList<T> list, int count, T defaultValue) {
			for (int i = list.Count; i < count; i++) {
				list.Add(defaultValue);
			}
			return list;
		}

		/// <summary>
		/// Extends the list to the specified size adding the default value.
		/// </summary>
		/// <typeparam name="T">The type of the list element</typeparam>
		/// <param name="list">The list to be extended</param>
		/// <param name="count">The size to extend</param>
		/// <returns>The extended list</returns>
		public static List<T> Extend<T>(this List<T> list, int count) {
			return Extend(list, count, default(T));
		}

		/// <summary>
		/// Extends the list to the specified size adding the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the list element</typeparam>
		/// <param name="list">The list to be extended</param>
		/// <param name="count">The size to extend</param>
		/// <param name="defaultValue">The value to be added</param>
		/// <returns>The extended list</returns>
		public static List<T> Extend<T>(
				this List<T> list, int count, T defaultValue) {
			Extend((IList<T>)list, count, defaultValue);
			return list;
		}

		/// <summary>
		/// Shuffles the specified array with the default <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the array.</typeparam>
		/// <param name="array">The array to be shuffled.</param>
		/// <returns>The shuffled array.</returns>
		public static T[] Shuffle<T>(this T[] array) {
			return array.Shuffle<T, T[]>(new Random());
		}

		/// <summary>
		/// Shuffles the specified array with the specified <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the array.</typeparam>
		/// <param name="array">The array to be shuffled.</param>
		/// <param name="random">The <see cref="Random"/> instace to shuffle.</param>
		/// <returns>The shuffled array.</returns>
		public static T[] Shuffle<T>(this T[] array, Random random) {
			return array.Shuffle<T, T[]>(random);
		}

		/// <summary>
		/// Shuffles the specified list with the default <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The list to be shuffled.</param>
		/// <returns>The shuffled list.</returns>
		public static List<T> Shuffle<T>(this List<T> list) {
			return list.Shuffle<T, List<T>>(new Random());
		}

		/// <summary>
		/// Shuffles the specified list with the specified <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The list to be shuffled.</param>
		/// <param name="random">The <see cref="Random"/> instace to shuffle.</param>
		/// <returns>The shuffled list.</returns>
		public static List<T> Shuffle<T>(this List<T> list, Random random) {
			return list.Shuffle<T, List<T>>(random);
		}

		/// <summary>
		/// Shuffles the specified list with the default <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The list to be shuffled.</param>
		/// <returns>The shuffled list.</returns>
		public static IList<T> Shuffle<T>(this IList<T> list) {
			return list.Shuffle<T, IList<T>>(new Random());
		}

		/// <summary>
		/// Shuffles the specified list with the specified <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <param name="list">The list to be shuffled.</param>
		/// <param name="random">The <see cref="Random"/> instace to shuffle.</param>
		/// <returns>The shuffled list.</returns>
		public static IList<T> Shuffle<T>(this IList<T> list, Random random) {
			return list.Shuffle<T, IList<T>>(random);
		}

		/// <summary>
		/// Shuffles the specified list with the default <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <typeparam name="TList">The type of the list.</typeparam>
		/// <param name="list">The list to be shuffled.</param>
		/// <returns>The shuffled list.</returns>
		public static TList Shuffle<T, TList>(this TList list)
				where TList : IList<T> {
			return list.Shuffle<T, TList>(new Random());
		}

		/// <summary>
		/// Shuffles the specified list with the specified <see cref="Random"/> using Fisher-Yates algorithm.
		/// </summary>
		/// <typeparam name="T">The type of elements in the list.</typeparam>
		/// <typeparam name="TList">The type of the list.</typeparam>
		/// <param name="list">The list to be shuffled.</param>
		/// <param name="random">The <see cref="Random"/> instace to shuffle.</param>
		/// <returns>The shuffled list.</returns>
		public static TList Shuffle<T, TList>(this TList list, Random random)
				where TList : IList<T> {
			int n = list.Count;
			while (n > 1) {
				int k = random.Next(n--);
				T temp = list[n];
				list[n] = list[k];
				list[k] = temp;
			}
			return list;
		}
	}
}