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

using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Algorithm {
    public static class Sort {
        private static void Merge<T>(
                IList<T> list, IComparer<T> cmp, int left, int center, int right,
                T[] work) {
            Contract.Requires(list != null);
            Contract.Requires(cmp != null);
            Contract.Requires(work != null);
            Contract.Requires(0 <= left);
            Contract.Requires(left < center);
            Contract.Requires(center < right);
            Contract.Requires(right <= list.Count);

            var leftMax = list[center - 1];
                    {
                        var tmp = list[center];
                        // 既に整列済みかどうか調べる（番兵的）
                        if (cmp.Compare(leftMax, tmp) <= 0) {
                            return;
                        }
                        // 左端の要素で移動する必要がないものはスキップする
                        while (cmp.Compare(list[left], tmp) <= 0) {
                            left++;
                        }
                    }

            // 一時的にコピー
            for (int i = left; i < center; i++) {
                work[i - left] = list[i];
            }

            int l = 0, r = center, o = left, endl = center - left;
            // 最後に移動する要素は左側か右側か
            if (cmp.Compare(leftMax, list[right - 1]) <= 0) {
                do {
                    if (cmp.Compare(work[l], list[r]) <= 0) {
                        list[o] = work[l++];
                    } else {
                        list[o] = list[r++];
                    }
                    o++;
                } while (l < endl); // 左側がなくなれば、残った右側はそのままで良い
            } else {
                int endr = right;
                do {
                    if (cmp.Compare(work[l], list[r]) <= 0) {
                        list[o] = work[l++];
                    } else {
                        list[o] = list[r++];
                    }
                    o++;
                } while (r < endr); // 右側がなくなるまで

                // 残った左側の移動
                do {
                    list[o++] = work[l++];
                } while (l < endl);
            }
        }

        public static void MergeSort<T>(
                IList<T> list, IComparer<T> cmp, int left, int right, T[] work) {
            Contract.Requires(list != null);
            Contract.Requires(cmp != null);
            Contract.Requires(0 <= left);
            Contract.Requires(right <= list.Count);
            Contract.Requires(work != null);
            Contract.Requires(work.Length >= list.Count >> 1);

            var n = right - left;
            if (n <= 16) {
                InsertionSort(list, cmp, left, right);
                return;
            }

            n >>= 1;
            MergeSort(list, cmp, left, left + n, work);
            MergeSort(list, cmp, left + n, right, work);

            Merge(list, cmp, left, left + n, right, work);
        }

        public static void MergeSort<T>(IList<T> list, IComparer<T> cmp) {
            Contract.Requires(list != null);
            Contract.Requires(cmp != null);

            var count = list.Count;
            var work = new T[count >> 1];
            MergeSort(list, cmp, 0, count, work);
        }

        public static void MergeSort<T>(IList<T> list) {
            Contract.Requires(list != null);

            MergeSort(list, Comparer<T>.Default);
        }

        public static void InsertionSort<T>(
                IList<T> list, IComparer<T> cmp, int left, int right) {
            Contract.Requires(list != null);
            Contract.Requires(cmp != null);
            Contract.Requires(0 <= left);
            Contract.Requires(right <= list.Count);

            for (int i = right - 2; i >= left; i--) {
                if (cmp.Compare(list[i], list[i + 1]) > 0) {
                    var tmp = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = tmp;
                }
            }
            for (int i = left + 2; i < right; i++) {
                var item = list[i];
                if (cmp.Compare(list[i - 1], item) > 0) {
                    var j = i - 1;
                    do {
                        list[j + 1] = list[j];
                        j--;
                    } while (cmp.Compare(list[j], item) > 0);
                    list[j + 1] = item;
                }
            }
        }
    }
}