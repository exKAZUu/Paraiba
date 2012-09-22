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
	public static class StringExtensions {
		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified character in the current String object.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">A Unicode character to seek.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(this string text, char value) {
			return IndexesOf(text, value, 0, text.Length);
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified character in the current String object.
		/// The search starts at a specified character position.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">A Unicode character to seek.</param>
		/// <param name="startIndex">The search	starting position.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(
				this string text, char value, int startIndex) {
			return IndexesOf(text, value, startIndex, text.Length);
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified character in the current String object.
		/// The search starts at a specified character position and examines a specified number of character positions.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">A Unicode character to seek.</param>
		/// <param name="startIndex">The search	starting position.</param>
		/// <param name="count">The number of character positions to examine.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(
				this string text, char value, int startIndex, int count) {
			int index;
			var endIndex = startIndex + count;
			while ((index = text.IndexOf(value, startIndex, endIndex - startIndex)) >= 0) {
				yield return index;
				startIndex = index + 1;
			}
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified string in the current String object.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">The string to seek.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(this string text, string value) {
			return IndexesOf(text, value, 0, text.Length, StringComparison.Ordinal);
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified string in the current String object.
		/// The search starts at a specified character position.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">The string to seek.</param>
		/// <param name="startIndex">The search	starting position.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(
				this string text, string value, int startIndex) {
			return IndexesOf(text, value, startIndex, text.Length, StringComparison.Ordinal);
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified string in the current String object.
		/// The search starts at a specified character position and examines a specified number of character positions.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">The string to seek.</param>
		/// <param name="startIndex">The search	starting position.</param>
		/// <param name="count">The number of character positions to examine.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(
				this string text, string value, int startIndex, int count) {
			return IndexesOf(text, value, startIndex, count, StringComparison.Ordinal);
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified string in the current <see cref="String" /> object.
		/// A parameter specifies the type of search to use for the specified string.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">The string to seek.</param>
		/// <param name="stringComparison">One of the enumeration values that specifies the rules for the search.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(this string text, string value, StringComparison stringComparison) {
			return IndexesOf(text, value, 0, text.Length, stringComparison);
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified string in the current <see cref="String" /> object.
		/// Parameters specify the starting search position in the current string and the type of search to use for the specified string.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">The string to seek.</param>
		/// <param name="startIndex">The search	starting position.</param>
		/// <param name="stringComparison">One of the enumeration values that specifies the rules for the search.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(
				this string text, string value, int startIndex, StringComparison stringComparison) {
			return IndexesOf(text, value, startIndex, text.Length, stringComparison);
		}

		/// <summary>
		/// Reports the zero-based indexes of the occurrence of the specified string in the current String object.
		/// Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.
		/// </summary>
		/// <param name="text">The string to be seek.</param>
		/// <param name="value">The string to seek.</param>
		/// <param name="startIndex">The search	starting position.</param>
		/// <param name="count">The number of character positions to examine.</param>
		/// <param name="stringComparison">One of the enumeration values that specifies the rules for the search.</param>
		/// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
		public static IEnumerable<int> IndexesOf(
				this string text, string value, int startIndex, int count, StringComparison stringComparison) {
			int index;
			var endIndex = startIndex + count;
			while ((index = text.IndexOf(value, startIndex, endIndex - startIndex, stringComparison)) >= 0) {
				yield return index;
				startIndex = index + 1;
			}
		}
	}
}