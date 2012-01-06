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
using System.Collections;
using System.Collections.Generic;
using Paraiba.Wrap;

namespace Paraiba.Utility {
    public class ReadonlyListWrapper<TItem>
            : ReadonlyListWrapper<IList<TItem>, TItem> {
        public ReadonlyListWrapper(IWrap<IList<TItem>> wrap)
                : base(wrap) {}
            }

    public class ReadonlyListWrapper<TList, TItem>
            : ReadonlyListWrapper<IWrap<TList>, TList, TItem>
            where TList : IList<TItem> {
        public ReadonlyListWrapper(IWrap<TList> wrap)
                : base(wrap) {}
            }

    public class ReadonlyListWrapper<TWrap, TList, TItem>
            : XReadonlyListWrapper<TWrap, TList, TItem, TItem>
            where TWrap : IWrap<TList>
            where TList : IList<TItem> {
        public ReadonlyListWrapper(TWrap wrap)
                : base(wrap) {}
            }

    public class XReadonlyListWrapper<TOrgItem, TItem>
            : XReadonlyListWrapper<IList<TOrgItem>, TOrgItem, TItem>
            where TOrgItem : TItem {
        public XReadonlyListWrapper(IWrap<IList<TOrgItem>> wrap)
                : base(wrap) {}
            }

    public class XReadonlyListWrapper<TList, TOrgItem, TItem>
            : XReadonlyListWrapper<IWrap<TList>, TList, TOrgItem, TItem>
            where TList : IList<TOrgItem>
            where TOrgItem : TItem {
        public XReadonlyListWrapper(IWrap<TList> wrap)
                : base(wrap) {}
            }

    /// <summary>
    ///   IListをラップしたIWrap型のオブジェクトを与えることで、 委譲により読み込み専用の IList インタフェースの実装を提供します。 なお、ジェネリクスの型引数における共変性も実現します。
    /// </summary>
    /// <typeparam name="TWrap"> </typeparam>
    /// <typeparam name="TList"> </typeparam>
    /// <typeparam name="TOrgItem"> </typeparam>
    /// <typeparam name="TItem"> </typeparam>
    public class XReadonlyListWrapper<TWrap, TList, TOrgItem, TItem>
            : IList<TItem>
            where TWrap : IWrap<TList>
            where TList : IList<TOrgItem>
            where TOrgItem : TItem {
        private readonly TWrap _wrap;

        public XReadonlyListWrapper(TWrap wrap) {
            _wrap = wrap;
        }

        public TWrap IWrap {
            get { return _wrap; }
        }

        public TList List {
            get { return _wrap.Value; }
        }

        #region IList<TItem> Members

        public void Add(TItem item) {
            throw new NotSupportedException("要素の追加はサポートされていない操作です。");
        }

        public void Clear() {
            throw new NotSupportedException("要素の削除はサポートされていない操作です。");
        }

        public bool Contains(TItem item) {
            if (item is TOrgItem
                ||
                (default(TOrgItem) == null && default(TItem) == null
                 && item == null)) {
                return _wrap.Value.Contains((TOrgItem)item);
            }
            return false;
        }

        public void CopyTo(TItem[] array, int arrayIndex) {
            var list = _wrap.Value;
            var endIndex = list.Count + arrayIndex;
            for (; arrayIndex < endIndex; arrayIndex++) {
                array[arrayIndex] = list[arrayIndex];
            }
        }

        public IEnumerator<TItem> GetEnumerator() {
            var list = _wrap.Value;
            foreach (var item in list) {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool Remove(TItem item) {
            throw new NotSupportedException("要素の削除はサポートされていない操作です。");
        }

        public int Count {
            get { return _wrap.Value.Count; }
        }

        public bool IsReadOnly {
            get { return true; }
        }

        public int IndexOf(TItem item) {
            if (item is TOrgItem
                ||
                (default(TOrgItem) == null && default(TItem) == null
                 && item == null)) {
                return _wrap.Value.IndexOf((TOrgItem)item);
            }
            return -1;
        }

        public void Insert(int index, TItem item) {
            throw new NotSupportedException("要素の追加はサポートされていない操作です。");
        }

        public void RemoveAt(int index) {
            throw new NotSupportedException("要素の削除はサポートされていない操作です。");
        }

        public TItem this[int index] {
            get { return _wrap.Value[index]; }
            set { throw new NotSupportedException("要素の更新はサポートされていない操作です。"); }
        }

        #endregion
            }
}