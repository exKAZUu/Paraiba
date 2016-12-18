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
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic {
    public static class LinkedListExtensions {
        public static LinkedListNode<T> Find<T>(
            this LinkedList<T> list, Predicate<T> predicate) {
            Contract.Requires(list != null);
            Contract.Requires(predicate != null);

            for (var node = list.First; node != null; node = node.Next) {
                if (predicate(node.Value)) {
                    return node;
                }
            }
            return null;
        }

        public static LinkedListNode<T> Find<T>(
            this LinkedList<T> list, T value, Func<T, T, int> comapreFunc) {
            Contract.Requires(list != null);
            Contract.Requires(comapreFunc != null);

            for (var node = list.First; node != null; node = node.Next) {
                if (comapreFunc(value, node.Value) == 0) {
                    return node;
                }
            }
            return null;
        }

        public static LinkedListNode<T> Find<T>(
            this LinkedList<T> list, T value, Func<T, T, bool> equalFunc) {
            Contract.Requires(list != null);
            Contract.Requires(equalFunc != null);

            for (var node = list.First; node != null; node = node.Next) {
                if (equalFunc(value, node.Value)) {
                    return node;
                }
            }
            return null;
        }

        public static LinkedListNode<T> Find<T>(
            this LinkedList<T> list, T value, IComparer<T> cmp) {
            Contract.Requires(list != null);
            Contract.Requires(cmp != null);

            for (var node = list.First; node != null; node = node.Next) {
                if (cmp.Compare(value, node.Value) == 0) {
                    return node;
                }
            }
            return null;
        }

        public static LinkedListNode<T> Find<T>(
            this LinkedList<T> list, T value, IEqualityComparer<T> eqcmp) {
            Contract.Requires(list != null);
            Contract.Requires(eqcmp != null);

            for (var node = list.First; node != null; node = node.Next) {
                if (eqcmp.Equals(value, node.Value)) {
                    return node;
                }
            }
            return null;
        }

        public static IEnumerable<LinkedListNode<T>> GetPreviousNodes<T>(
            this LinkedListNode<T> from) {
            while (from != null) {
                yield return from;
                from = from.Previous;
            }
        }

        public static IEnumerable<LinkedListNode<T>> GetPreviousNodes<T>(
            this LinkedListNode<T> from, LinkedListNode<T> to) {
            while (from != to) {
                yield return from;
                from = from.Previous;
            }
            yield return from;
        }

        public static IEnumerable<LinkedListNode<T>> GetNextNodes<T>(
            this LinkedListNode<T> from) {
            while (from != null) {
                yield return from;
                from = from.Next;
            }
        }

        public static IEnumerable<LinkedListNode<T>> GetNextNodes<T>(
            this LinkedListNode<T> from, LinkedListNode<T> to) {
            while (from != to) {
                yield return from;
                from = from.Next;
            }
            yield return from;
        }
    }
}