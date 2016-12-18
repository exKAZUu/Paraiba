#region License

// Copyright (C) 2011-2016 Kazunori Sakamoto
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
        /// Returns the similarity of the specified two strings.
        /// </summary>
        /// <param name="str1">The first string to be calculated.</param>
        /// <param name="str2">The second string to be calculated.</param>
        /// <returns>The similarity of the specified two strings.</returns>
        public static int CalculateSimilarity(this string str1, string str2) {
            var table = new int[str1.Length + 1, str2.Length + 1];
            for (int i = 0; i <= str1.Length; i++) {
                table[i, 0] = i;
            }
            for (int j = 0; j <= str2.Length; j++) {
                table[0, j] = j;
            }
            for (int i = 1; i <= str1.Length; i++) {
                for (int j = 1; j <= str2.Length; j++) {
                    var min = Math.Min(table[i - 1, j], table[i, j - 1]) + 1;
                    if (str1[i - 1] == str2[j - 1]) {
                        min = Math.Min(min, table[i - 1, j - 1]);
                    }
                    table[i, j] = min;
                }
            }
            return -table[str1.Length, str2.Length];
        }

        /// <summary>
        /// Returns the substring that has the same prefix of the specified string.
        /// </summary>
        /// <param name="str">The string whose substring is returned.</param>
        /// <param name="length">The length of the substring.</param>
        /// <returns>The substring that has the same prefix of the specified string.</returns>
        public static string Left(this string str, int length) {
            return str.Substring(0, length);
        }

        /// <summary>
        /// Returns the substring that has the same suffix of the specified string.
        /// </summary>
        /// <param name="str">The string whose substring is returned.</param>
        /// <param name="length">The length of the substring.</param>
        /// <returns>The substring that has the same suffix of the specified string.</returns>
        public static string Right(this string str, int length) {
            return str.Substring(str.Length - length, length);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified character in the current String object.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">A Unicode character to seek.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(this string text, char value) {
            return IndicesOf(text, value, 0, text.Length);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified character in the current String object.
        /// The search starts at a specified character position.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">A Unicode character to seek.</param>
        /// <param name="startIndex">The search	starting position.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(
            this string text, char value, int startIndex) {
            return IndicesOf(text, value, startIndex, text.Length);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified character in the current String object.
        /// The search starts at a specified character position and examines a specified number of character positions.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">A Unicode character to seek.</param>
        /// <param name="startIndex">The search	starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(
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
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">The string to seek.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(this string text, string value) {
            return IndicesOf(text, value, 0, text.Length, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified string in the current String object.
        /// The search starts at a specified character position.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search	starting position.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(
            this string text, string value, int startIndex) {
            return IndicesOf(text, value, startIndex, text.Length, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified string in the current String object.
        /// The search starts at a specified character position and examines a specified number of character positions.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search	starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(
            this string text, string value, int startIndex, int count) {
            return IndicesOf(text, value, startIndex, count, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified string in the current <see cref="String" /> object.
        /// A parameter specifies the type of search to use for the specified string.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="stringComparison">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(
            this string text, string value, StringComparison stringComparison) {
            return IndicesOf(text, value, 0, text.Length, stringComparison);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified string in the current <see cref="String" /> object.
        /// Parameters specify the starting search position in the current string and the type of search to use for the specified string.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search	starting position.</param>
        /// <param name="stringComparison">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(
            this string text, string value, int startIndex, StringComparison stringComparison) {
            return IndicesOf(text, value, startIndex, text.Length, stringComparison);
        }

        /// <summary>
        /// Reports the zero-based indexes of the occurrence of the specified string in the current String object.
        /// Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.
        /// </summary>
        /// <param name="text">The string to be seeked.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search	starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="stringComparison">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>The zero-based index position of the <c>value</c> parameter if that string is found, or -1 if it is not. If <c>value</c> is <see cref="Empty" />, the return value is <c>startIndex</c>.</returns>
        public static IEnumerable<int> IndicesOf(
            this string text, string value, int startIndex, int count,
            StringComparison stringComparison) {
            int index;
            var endIndex = startIndex + count;
            while (
                (index =
                        text.IndexOf(value, startIndex, endIndex - startIndex, stringComparison))
                >= 0) {
                yield return index;
                startIndex = index + 1;
            }
        }

        /// <summary>
        /// Returns a new string in which newlines for unix-like systems are replaced with ones for windows.
        /// </summary>
        /// <param name="text">The string to be replaced.</param>
        /// <returns>The new string.</returns>
        public static string ReplaceNewlinesForUnix(this string text) {
            return text.Replace("\r\n", "\n");
        }

        /// <summary>
        /// Returns a new string in which newlines for windws are replaced with ones for unix-like systems.
        /// </summary>
        /// <param name="text">The string to be replaced.</param>
        /// <returns>The new string.</returns>
        public static string ReplaceNewlinesForWindows(this string text) {
            return text.Replace("\r\n", "\n").Replace("\n", "\r\n");
        }
    }
}