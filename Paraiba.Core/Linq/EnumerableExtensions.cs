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
using System.Linq;

namespace Paraiba.Linq {
    /// <summary>
    /// Provides a set of <c>static</c> methods for querying objects that implement <see cref="IEnumerable(Of T)"/>.
    /// </summary>
    public static class EnumerableExtensions {
        /// <summary>
        /// Applies an accumulator function over a sequence without the first element.
        /// The specified function is used to transform the first element into the initial accumulator value.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TAccumulate">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable(Of T)"/> to aggregate over.</param>
        /// <param name="firstSelector">A function to transform the first element into the initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <returns></returns>
        public static TAccumulate AggregateApartFirst<T, TAccumulate>(
                this IEnumerable<T> source, Func<T, TAccumulate> firstSelector,
                Func<TAccumulate, T, TAccumulate> func) {
            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    throw new InvalidOperationException("The specified sequence is empty.");
                }
                var seed = firstSelector(enumerator.Current);
                while (enumerator.MoveNext()) {
                    seed = func(seed, enumerator.Current);
                }
                return seed;
            }
        }

        /// <summary>
        /// Applies an accumulator function over a sequence without the first element.
        /// The specified function is used to transform the first element into the initial accumulator value, and the specified function is used to select the result value.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TAccumulate">The type of the elements of <c>source</c>.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">An <see cref="IEnumerable(Of T)"/> to aggregate over.</param>
        /// <param name="firstSelector">A function to transform the first element into the initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
        /// <returns>The transformed final accumulator value.</returns>
        public static TResult AggregateApartFirst<T, TAccumulate, TResult>(
                this IEnumerable<T> source, Func<T, TAccumulate> firstSelector,
                Func<TAccumulate, T, TAccumulate> func,
                Func<TAccumulate, TResult> resultSelector) {
            using (var enumerator = source.GetEnumerator()) {
                if (!enumerator.MoveNext()) {
                    throw new InvalidOperationException("The specified sequence is empty.");
                }
                var seed = firstSelector(enumerator.Current);
                while (enumerator.MoveNext()) {
                    seed = func(seed, enumerator.Current);
                }
                return resultSelector(seed);
            }
        }

        /// <summary>
        ///   指定したシーケンスを逆順にしてAggregateを計算します．
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source"> </param>
        /// <param name="func"> </param>
        /// <returns> </returns>
        public static T AggregateRight<T>(
                this IEnumerable<T> source, Func<T, T, T> func) {
            return source.Reverse().Aggregate(func);
        }

        /// <summary>
        ///   指定したシーケンスを逆順にしてAggregateを計算します．
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <typeparam name="TAccumulate"> </typeparam>
        /// <param name="source"> </param>
        /// <param name="seed"> </param>
        /// <param name="func"> </param>
        /// <returns> </returns>
        public static TAccumulate AggregateRight<T, TAccumulate>(
                this IEnumerable<T> source, TAccumulate seed,
                Func<TAccumulate, T, TAccumulate> func) {
            return source.Reverse().Aggregate(seed, func);
        }

        /// <summary>
        ///   指定したシーケンスを要素数が2個のタプル列に分解します．
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source"> </param>
        /// <returns> </returns>
        public static IEnumerable<Tuple<T, T>> Split2<T>(
                this IEnumerable<T> source) {
            using (var enumerator = source.GetEnumerator()) {
                while (enumerator.MoveNext()) {
                    var item1 = enumerator.Current;
                    if (!enumerator.MoveNext()) {
                        yield break;
                    }
                    var item2 = enumerator.Current;
                    yield return Tuple.Create(item1, item2);
                }
            }
        }
    }
}