#region License

// Copyright (C) 2008-2012 Kazunori Sakamoto
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
    /// <summary>
    /// Provides a set of <c>static</c> methods for indexed elements
    /// that implement <see cref="IList(Of T)" /> to search elements.
    /// </summary>
    public static class BinarySearchExtensions {
        /// <summary>
        /// Returns an index indicating lower bound for a sorted list using the default comparer.
        /// This method is an O(log N) operation.
        /// E.g. given [1, 1, 3, 3] and [0,1], it returns 0.
        /// E.g. given [1, 1, 3, 3] and [2,3], it returns 2.
        /// E.g. given [1, 1, 3, 3] and [4- ], it returns 4.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c>list</c>.</typeparam>
        /// <param name="list">The list where the element is searched.</param>
        /// <param name="element">The element to search.</param>
        /// <returns>The found lower index.</returns>
        public static int FindLowerBound<T>(this IList<T> list, T element)
                where T : IComparable<T> {
            return FindLowerBound(list, element, Comparer<T>.Default);
        }

        /// <summary>
        /// Returns an index indicating lower bound for a sorted list using the given comparer.
        /// This method is an O(log N) operation.
        /// E.g. given [1, 1, 3, 3] and [0,1], it returns 0.
        /// E.g. given [1, 1, 3, 3] and [2,3], it returns 2.
        /// E.g. given [1, 1, 3, 3] and [4- ], it returns 4.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c>list</c>.</typeparam>
        /// <param name="list">The list where the element is searched.</param>
        /// <param name="element">The element to search.</param>
        /// <param name="comparer">The comparer to search the element.</param>
        /// <returns>The found lower index.</returns>
        public static int FindLowerBound<T>(
                this IList<T> list, T element, IComparer<T> comparer) {
            var start = -1;
            var end = list.Count;
            while (end - start > 1) {
                var mid = (start + end) / 2;
                if (comparer.Compare(list[mid], element) >= 0) {
                    end = mid;
                } else {
                    start = mid;
                }
            }
            return end;
        }

        /// <summary>
        /// Returns an index indicating upper bound for a sorted list using the default comparer.
        /// This method is an O(log N) operation.
        /// E.g. given [1, 1, 3, 3] and [ -0], it returns -1.
        /// E.g. given [1, 1, 3, 3] and [1,2], it returns 1.
        /// E.g. given [1, 1, 3, 3] and [3,4], it returns 3.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c>list</c>.</typeparam>
        /// <param name="list">The list where the element is searched.</param>
        /// <param name="element">The element to search.</param>
        /// <returns>The found upper index.</returns>
        public static int FindUpperBound<T>(this IList<T> list, T element)
                where T : IComparable<T> {
            return FindUpperBound(list, element, Comparer<T>.Default);
        }

        /// <summary>
        /// Returns an index indicating upper bound for a sorted list using the given comparer.
        /// This method is an O(log N) operation.
        /// E.g. given [1, 1, 3, 3] and [ -0], it returns -1.
        /// E.g. given [1, 1, 3, 3] and [1,2], it returns 1.
        /// E.g. given [1, 1, 3, 3] and [3,4], it returns 3.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c>list</c>.</typeparam>
        /// <param name="list">The list where the element is searched.</param>
        /// <param name="element">The element to search.</param>
        /// <param name="comparer">The comparer to search the element.</param>
        /// <returns>The found upper index.</returns>
        public static int FindUpperBound<T>(
                this IList<T> list, T element, IComparer<T> comparer) {
            var start = -1;
            var end = list.Count;
            while (end - start > 1) {
                var mid = (start + end) / 2;
                if (comparer.Compare(list[mid], element) > 0) {
                    end = mid;
                } else {
                    start = mid;
                }
            }
            return start;
        }
    }
}