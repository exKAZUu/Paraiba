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

namespace Paraiba.Collections.Generic {
    public static class ListExtensions2 {
        public static bool SequenceEqual<T>(
                this IList<T> list, IEnumerable<T> that, int startIndex = 0) {
            return
                    that.All(
                            item =>
                            startIndex < list.Count
                            && list[startIndex++].Equals(item))
                    && startIndex == list.Count;
        }

        public static bool IsSubSequence<T>(
                this ICollection<T> collection, IList<T> parentSequence) {
            var count = parentSequence.Count;
            var subCount = collection.Count;
            for (int i = 0; i <= count - subCount; i++) {
                if (parentSequence.SequenceEqual(collection, i)) {
                    return true;
                }
            }
            return false;
        }

        public static T AtOrDefault<T>(this IList<T> list, int index) {
            return 0 <= index && index < list.Count()
                           ? list[index] : default(T);
        }

        public static IEnumerable<T> AsReverse<T>(this IList<T> list) {
            for (int i = list.Count - 1; i >= 0; i--) {
                yield return list[i];
            }
        }

        public static IEnumerable<T> GetEnumerable<T>(
                this IList<T> list, int startIndex) {
            for (int i = startIndex; i < list.Count; i++) {
                yield return list[i];
            }
        }

        public static IEnumerable<IList<T>> GetPermutations<T>(
                this IList<T> list) {
            return list.ToArray().GetPermutationsDestructively();
        }

        public static IEnumerable<IList<T>> GetPermutations<T>(
                this IList<T> list, IList<T> outList) {
            return list.GetPermutations(list.Count, outList);
        }

        public static IEnumerable<IList<T>> GetPermutations<T>(
                this IList<T> list, int N) {
            var array = new T[N];
            for (int i = 0; i < N; i++) {
                array[i] = list[i];
            }
            return array.GetPermutationsDestructively(N);
        }

        public static IEnumerable<IList<T>> GetPermutations<T>(
                this IList<T> list, int N, IList<T> outList) {
            for (int i = 0; i < N; i++) {
                outList[i] = list[i];
            }
            return outList.GetPermutationsDestructively(N);
        }

        public static IEnumerable<IList<T>> GetPermutationsDestructively<T>(
                this IList<T> list) {
            return list.GetPermutationsDestructively(list.Count);
        }

        public static IEnumerable<IList<T>> GetPermutationsDestructively<T>(
                this IList<T> list, int N) {
            // 階乗進法でN!を表現する
            var factSystem = new int[N + 1];
            for (int i = 0; i <= N; i++) {
                factSystem[i] = i;
            }

            int nDigit = 1;
            while (nDigit < N) {
                // 置換する対象を決める
                int target;
                if ((nDigit & 1) != 0) // 奇数かどうか
                {
                    target = factSystem[nDigit];
                } else {
                    target = 0;
                }

                // 置換
                var t = list[nDigit];
                list[nDigit] = list[target];
                list[target] = t;

                // 生成した順列を返す
                yield return list;

                // 階乗進法で表現された値を1減らす
                nDigit = 1;
                while (factSystem[nDigit] == 0) {
                    factSystem[nDigit] = nDigit;
                    nDigit++;
                }
                factSystem[nDigit]--;
            }
        }

        public static int ForEachDestructively<T>(
                this IList<T> list, Func<T, T> func) {
            int index = 0, count = list.Count;
            for (; index < count; index++) {
                list[index] = func(list[index]);
                index++;
            }
            return index;
        }

        public static IList<T> StableSort<T>(this IList<T> list) {
            return null;
        }

        public static IList<T> StableSort<T>(
                this IList<T> list, IComparer<T> cmp) {
            return null;
        }

        public static DisposableList<T> ToDisposable<T>(this IList<T> list)
                where T : IDisposable {
            return new DisposableList<T>(list);
        }

        public static T First<T>(this IList<T> list) {
            return list[0];
        }

        public static T Last<T>(this IList<T> list) {
            return list[list.Count - 1];
        }

        /// <summary>
        ///   指定したリストの指定したインデックスの要素を取得します．
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="index"> </param>
        /// <returns> </returns>
        public static T ElementAtOrDefault<T>(this IList<T> list, int index) {
            return 0 <= index && index < list.Count
                           ? list[index]
                           : default(T);
        }

        /// <summary>
        ///   指定したリストの逆順のシーケンスを取得します．
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"> </param>
        /// <returns> </returns>
        public static IEnumerable<T> Reverse<T>(this IList<T> list) {
            for (int i = list.Count - 1; i >= 0; i--) {
                yield return list[i];
            }
        }

        /// <summary>
        ///   リストにデフォルト値を追加して、有効要素数を拡張します．
        /// </summary>
        /// <param name="count"> 拡張するリストのサイズ </param>
        public static IList<T> Extend<T>(this IList<T> list, int count) {
            return Extend(list, count, default(T));
        }

        /// <summary>
        ///   リストに指定した要素を追加して、有効要素数を拡張します．
        /// </summary>
        /// <param name="count"> 拡張するリストのサイズ </param>
        /// <param name="defaultElement"> 拡張する際に追加する要素 </param>
        public static IList<T> Extend<T>(
                this IList<T> list, int count, T defaultElement) {
            for (int i = list.Count; i < count; i++) {
                list.Add(defaultElement);
            }
            return list;
        }

        /// <summary>
        ///   リストに指定した要素を追加して、有効要素数を拡張します．
        /// </summary>
        /// <param name="count"> 拡張するリストのサイズ </param>
        /// <param name="defaultElementFunc"> 拡張する際に追加する要素を取得するデリゲート </param>
        public static List<T> Extend<T>(
                this List<T> list, int count, Func<T> defaultElementFunc) {
            for (int i = list.Count; i < count; i++) {
                list.Add(defaultElementFunc());
            }
            return list;
        }

        /// <summary>
        ///   リストの全要素に指定した要素を設定します．
        /// </summary>
        /// <typeparam name="TValue"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="element"> </param>
        /// <returns> </returns>
        public static IList<T> Fill<T>(this IList<T> list, T element) {
            var count = list.Count;
            for (int i = 0; i < count; i++) {
                list[i] = element;
            }
            return list;
        }

        /// <summary>
        ///   リストの各要素に指定したデリゲートの戻り値を設定します．
        /// </summary>
        /// <typeparam name="TValue"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="element"> </param>
        /// <returns> </returns>
        public static IList<T> Fill<T>(this IList<T> list, Func<T> func) {
            for (int i = 0; i < list.Count; i++) {
                list[i] = func();
            }
            return list;
        }

        /// <summary>
        ///   リストの各要素に指定したデリゲートの戻り値を設定します．
        /// </summary>
        /// <typeparam name="TValue"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="element"> </param>
        /// <returns> </returns>
        public static IList<T> Fill<T>(this IList<T> list, Func<int, T> func) {
            for (int i = 0; i < list.Count; i++) {
                list[i] = func(i);
            }
            return list;
        }
    }
}