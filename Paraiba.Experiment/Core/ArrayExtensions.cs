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

namespace Paraiba.Core {
	public static class ArrayExtensions {
		public static T AtOrDefault<T>(this T[] array, int index) {
			return 0 <= index && index < array.Length
					? array[index]
					: default(T);
		}

		public static T At<T>(this T[][] array, Tuple<int, int> index) {
			return array[index.Item1][index.Item2];
		}

		public static T At<T>(this T[,] array, Tuple<int, int> index) {
			return array[index.Item1, index.Item2];
		}

		public static T At<T>(this T[][][] array, Tuple<int, int, int> index) {
			return array[index.Item1][index.Item2][index.Item3];
		}

		public static T At<T>(this T[,,] array, Tuple<int, int, int> index) {
			return array[index.Item1, index.Item2, index.Item3];
		}

		//public static void Clone<TValue>(this TValue[] array)
		//{
		//    var newArray = new TValue[array.Length];
		//    Array.Copy(array, newArray, array.Length);
		//    return newArray;
		//}

		public static void ForEach<T>(this T[,] array, Action<T> action) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int r = 0; r < nRow; r++) {
				for (int c = 0; c < nColumn; c++) {
					action(array[c, r]);
				}
			}
		}

		public static void ForEach<T>(
				this T[,] array, Action<T, int, int> action) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int r = 0; r < nRow; r++) {
				for (int c = 0; c < nColumn; c++) {
					action(array[c, r], c, r);
				}
			}
		}

		public static void ForEachReverse<T>(this T[,] array, Action<T> action) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int c = 0; c < nColumn; c++) {
				for (int r = 0; r < nRow; r++) {
					action(array[c, r]);
				}
			}
		}

		public static void ForEachReverse<T>(
				this T[,] array, Action<T, int, int> action) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int c = 0; c < nColumn; c++) {
				for (int r = 0; r < nRow; r++) {
					action(array[c, r], c, r);
				}
			}
		}

		public static IEnumerable<T> GetElements<T>(this T[,] array) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int r = 0; r < nRow; r++) {
				for (int c = 0; c < nColumn; c++) {
					yield return array[c, r];
				}
			}
		}

		public static IEnumerable<T> GetElementsReverse<T>(this T[,] array) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int c = 0; c < nColumn; c++) {
				for (int r = 0; r < nRow; r++) {
					yield return array[c, r];
				}
			}
		}

		public static IEnumerable<TResult> Select<T, TResult>(
				this T[,] array, Func<T, int, int, TResult> func) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int r = 0; r < nRow; r++) {
				for (int c = 0; c < nColumn; c++) {
					yield return func(array[c, r], c, r);
				}
			}
		}

		public static IEnumerable<TResult> SelectReverse<T, TResult>(
				this T[,] array, Func<T, int, int, TResult> func) {
			var nRow = array.GetLength(1);
			var nColumn = array.GetLength(0);
			for (int c = 0; c < nColumn; c++) {
				for (int r = 0; r < nRow; r++) {
					yield return func(array[c, r], c, r);
				}
			}
		}

		/// <summary>
		///   指定した二次元配列の指定した行の値の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <param name="rowIndex"> 取得する行のインデックス </param>
		/// <returns> 行の値の列挙子 </returns>
		public static IEnumerable<T> GetRow<T>(this T[,] matrix, int rowIndex) {
			return GetRow(matrix, rowIndex, 0, matrix.GetLength(1));
		}

		/// <summary>
		///   指定した二次元配列の指定した行の範囲内の値の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <param name="rowIndex"> 取得する行のインデックス </param>
		/// <param name="columnLength"> 取得する範囲の先頭位置を示す列の要素数 </param>
		/// <returns> 行の値の列挙子 </returns>
		public static IEnumerable<T> GetRow<T>(
				T[,] matrix, int rowIndex, int columnLength) {
			return GetRow(matrix, rowIndex, 0, columnLength);
		}

		/// <summary>
		///   指定した二次元配列の指定した行の範囲内の値の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <param name="rowIndex"> 取得する行のインデックス </param>
		/// <param name="columnStartIndex"> 取得する範囲の先頭位置を示す列のインデックス </param>
		/// <param name="columnLength"> 取得する範囲内にある列の要素数 </param>
		/// <returns> 行の値の列挙子 </returns>
		public static IEnumerable<T> GetRow<T>(
				T[,] matrix, int rowIndex, int columnStartIndex,
				int columnLength) {
			for (; columnStartIndex < columnLength; columnStartIndex++) {
				yield return matrix[rowIndex, columnStartIndex];
			}
		}

		/// <summary>
		///   指定した二次元配列の行の値の列挙子の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <returns> 行の値の列挙子列挙子 </returns>
		public static IEnumerable<IEnumerable<T>> GetRows<T>(this T[,] matrix) {
			var nRow = matrix.GetLength(1);
			var nColumn = matrix.GetLength(0);
			for (int c = 0; c < nColumn; c++) {
				yield return GetRow(matrix, c, 0, nRow);
			}
		}

		/// <summary>
		///   指定した二次元配列の指定した列の値の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <param name="columnIndex"> 取得する列のインデックス </param>
		/// <returns> 列の値の列挙子 </returns>
		public static IEnumerable<T> GetColumn<T>(
				this T[,] matrix, int columnIndex) {
			return GetColumn(matrix, columnIndex, 0, matrix.GetLength(0));
		}

		/// <summary>
		///   指定した二次元配列の指定した列の値の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <param name="columnIndex"> 取得する列のインデックス </param>
		/// <param name="rowLength"> 取得する範囲内にある列の要素数 </param>
		/// <returns> 列の値の列挙子 </returns>
		public static IEnumerable<T> GetColumn<T>(
				T[,] matrix, int columnIndex, int rowLength) {
			return GetColumn(matrix, columnIndex, 0, rowLength);
		}

		/// <summary>
		///   指定した二次元配列の指定した列の値の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <param name="columnIndex"> 取得する列のインデックス </param>
		/// <param name="rowIndex"> 取得する範囲の先頭位置を示す列のインデック </param>
		/// <param name="rowLength"> 取得する範囲内にある列の要素数 </param>
		/// <returns> 列の値の列挙子 </returns>
		public static IEnumerable<T> GetColumn<T>(
				T[,] matrix, int columnIndex, int rowIndex, int rowLength) {
			for (; rowIndex < rowLength; rowIndex++) {
				yield return matrix[rowIndex, columnIndex];
			}
		}

		/// <summary>
		///   指定した二次元配列の列の値の列挙子の列挙子を返します。
		/// </summary>
		/// <typeparam name="TValue"> </typeparam>
		/// <param name="matrix"> 列挙子を取得する二次元配列 </param>
		/// <returns> 列の値の列挙子列挙子 </returns>
		public static IEnumerable<IEnumerable<T>> GetColumns<T>(
				this T[,] matrix) {
			var nRow = matrix.GetLength(1);
			var nColumn = matrix.GetLength(0);
			for (int r = 0; r < nRow; r++) {
				yield return GetColumn(matrix, r, nColumn);
			}
		}

		public static bool IsValidIndex<T>(this T[] array, int index) {
			return 0 <= index && index < array.Length;
		}

		public static bool IsValidIndex<T>(
				this T[,] array, int index0, int index1) {
			return 0 <= index0 && index0 < array.GetLength(0) && 0 <= index1
					&& index1 <= array.GetLength(1);
		}
	}
}