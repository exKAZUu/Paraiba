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

namespace Paraiba.Collections.Generic.Compare {
    public static class ComparerExtensions {
        public static IComparer<T> ToReverse<T>(
            this ReverseComparer<T> comparer) {
            Contract.Requires(comparer != null);

            return comparer.OriginalComparer;
        }

        public static ReverseComparer<T> ToReverse<T>(
            this IComparer<T> comparer) {
            Contract.Requires(comparer != null);

            return new ReverseComparer<T>(comparer);
        }

        public static NonGenericComparer<T> ToNonGeneric<T>(
            this IComparer<T> comparer) {
            Contract.Requires(comparer != null);

            return new NonGenericComparer<T>(comparer);
        }

        public static ComparerWithComparison<T> ToComparer<T>(
            this Comparison<T> compareFunc) {
            Contract.Requires(compareFunc != null);

            return new ComparerWithComparison<T>(compareFunc);
        }

        public static ComparerWithFunc<T> ToComparer<T>(
            this Func<T, T, int> compareFunc) {
            Contract.Requires(compareFunc != null);

            return new ComparerWithFunc<T>(compareFunc);
        }

        public static PartialComparer<TSource, TKey> ToComparer<TSource, TKey>(
            this Func<TSource, TKey> selector)
            where TKey : IComparable<TKey> {
            Contract.Requires(selector != null);

            return new PartialComparer<TSource, TKey>(selector);
        }
    }
}