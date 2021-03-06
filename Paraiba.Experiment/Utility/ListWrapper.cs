﻿#region License

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

using System.Collections;
using System.Collections.Generic;
using Paraiba.Wrap;

namespace Paraiba.Utility {
    public class ListWrapper<T> : ListWrapper<IList<T>, T> {
        public ListWrapper(IWrap<IList<T>> wrap)
            : base(wrap) {}
    }

    public class ListWrapper<TList, T> : ListWrapper<IWrap<TList>, TList, T>
            where TList : IList<T> {
        public ListWrapper(IWrap<TList> wrap)
            : base(wrap) {}
    }

    /// <summary>
    ///   IListをラップしたIWrap型のオブジェクトを与えることで、 委譲により IList インタフェースの実装を提供します。
    /// </summary>
    /// <remarks>
    ///   Adaptor pattern Adaptee: TWrap Adaptor: 本クラス Target: IList
    /// </remarks>
    /// <typeparam name="TWrap"> </typeparam>
    /// <typeparam name="TList"> </typeparam>
    /// <typeparam name="TValue"> </typeparam>
    /// <seealso>http://kurusugawa.jp/blog/archives/585/</seealso>
    public class ListWrapper<TWrap, TList, T> : IList<T>
            where TWrap : IWrap<TList>
            where TList : IList<T> {
        public ListWrapper(TWrap wrap) {
            IWrap = wrap;
        }

        public TWrap IWrap { get; }

        public TList List {
            get { return IWrap.Value; }
        }

        #region IList<T> Members

        public void Add(T item) {
            IWrap.Value.Add(item);
        }

        public void Clear() {
            IWrap.Value.Clear();
        }

        public bool Contains(T item) {
            return IWrap.Value.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            IWrap.Value.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() {
            return IWrap.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool Remove(T item) {
            return IWrap.Value.Remove(item);
        }

        public int Count {
            get { return IWrap.Value.Count; }
        }

        public bool IsReadOnly {
            get { return IWrap.Value.IsReadOnly; }
        }

        public int IndexOf(T item) {
            return IWrap.Value.IndexOf(item);
        }

        public void Insert(int index, T item) {
            IWrap.Value.Insert(index, item);
        }

        public void RemoveAt(int index) {
            IWrap.Value.RemoveAt(index);
        }

        public T this[int index] {
            get { return IWrap.Value[index]; }
            set { IWrap.Value[index] = value; }
        }

        #endregion
    }
}