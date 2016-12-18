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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections {
    public class DisposableList<T> : IList<T>, IDisposable
            where T : IDisposable {
        private IList<T> _list;

        public DisposableList(IList<T> list) {
            Contract.Requires(list != null);

            _list = list;
        }

        public bool Disposed {
            get { return _list == null; }
        }

        #region IDisposable Members

        ///<summary>
        ///  アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose() {
            if (_list == null) {
                return;
            }

            foreach (var item in _list) {
                if (item != null) {
                    item.Dispose();
                }
            }
            _list = null;
        }

        #endregion

        #region IList<T> Members

        ///<summary>
        ///  コレクションを反復処理する列挙子を返します。
        ///</summary>
        ///<returns> コレクションを反復処理するために使用できる <see cref="TValue:System.Collections.Generic.IEnumerator`1" /> 。 </returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator() {
            return _list.GetEnumerator();
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> に項目を追加します。
        ///</summary>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> に追加するオブジェクト。 </param>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  は読み取り専用です。</exception>
        public void Add(T item) {
            _list.Add(item);
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> からすべての項目を削除します。
        ///</summary>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  は読み取り専用です。</exception>
        public void Clear() {
            _list.Clear();
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> に特定の値が格納されているかどうかを判断します。
        ///</summary>
        ///<returns> <paramref name="item" /> が <see cref="TValue:System.Collections.Generic.ICollection`1" /> に存在する場合は true。それ以外の場合は false。 </returns>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> 内で検索するオブジェクト。 </param>
        public bool Contains(T item) {
            return _list.Contains(item);
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> の要素を <see cref="TValue:System.Array" /> にコピーします。 <see
        ///   cref="TValue:System.Array" /> の特定のインデックスからコピーが開始されます。
        ///</summary>
        ///<param name="array"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> から要素がコピーされる 1 次元の <see
        ///   cref="TValue:System.Array" /> 。 <see cref="TValue:System.Array" /> には、0 から始まるインデックス番号が必要です。 </param>
        ///<param name="arrayIndex"> コピーの開始位置となる、 <paramref name="array" /> の 0 から始まるインデックス番号。 </param>
        ///<exception cref="TValue:System.ArgumentNullException">
        ///  <paramref name="array" />
        ///  が null です。</exception>
        ///<exception cref="TValue:System.ArgumentOutOfRangeException">
        ///  <paramref name="arrayIndex" />
        ///  が 0 未満です。</exception>
        ///<exception cref="TValue:System.ArgumentException">
        ///  <paramref name="array" />
        ///  が多次元です。
        ///  または
        ///  <paramref name="arrayIndex" />
        ///  が array の長さ以上です。
        ///  または
        ///  コピー元の
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  の要素数が、
        ///  <paramref name="arrayIndex" />
        ///  からコピー先の
        ///  <paramref name="array" />
        ///  の末尾までに格納できる数を超えています。
        ///  または
        ///  型
        ///  <paramref name="TValue" />
        ///  をコピー先の
        ///  <paramref name="array" />
        ///  の型に自動的にキャストすることはできません。</exception>
        public void CopyTo(T[] array, int arrayIndex) {
            _list.CopyTo(array, arrayIndex);
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> 内で最初に見つかった特定のオブジェクトを削除します。
        ///</summary>
        ///<returns> <paramref name="item" /> が <see cref="TValue:System.Collections.Generic.ICollection`1" /> から正常に削除された場合は true。それ以外の場合は false。このメソッドは、 <paramref
        ///   name="item" /> が元の <see cref="TValue:System.Collections.Generic.ICollection`1" /> に見つからない場合にも false を返します。 </returns>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.ICollection`1" /> から削除するオブジェクト。 </param>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" />
        ///  は読み取り専用です。</exception>
        public bool Remove(T item) {
            return _list.Remove(item);
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> に格納されている要素の数を取得します。
        ///</summary>
        ///<returns> <see cref="TValue:System.Collections.Generic.ICollection`1" /> に格納されている要素の数。 </returns>
        public int Count {
            get { return _list.Count; }
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.ICollection`1" /> が読み取り専用かどうかを示す値を取得します。
        ///</summary>
        ///<returns> <see cref="TValue:System.Collections.Generic.ICollection`1" /> が読み取り専用の場合は true。それ以外の場合は false。 </returns>
        public bool IsReadOnly {
            get { return _list.IsReadOnly; }
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.IList`1" /> 内での指定した項目のインデックスを調べます。
        ///</summary>
        ///<returns> リストに存在する場合は <paramref name="item" /> のインデックス。それ以外の場合は -1。 </returns>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.IList`1" /> 内で検索するオブジェクト。 </param>
        public int IndexOf(T item) {
            return _list.IndexOf(item);
        }

        ///<summary>
        ///  <see cref="TValue:System.Collections.Generic.IList`1" /> の指定したインデックス位置に項目を挿入します。
        ///</summary>
        ///<param name="n"> <paramref name="item" /> を挿入する位置の、0 から始まるインデックス番号。 </param>
        ///<param name="item"> <see cref="TValue:System.Collections.Generic.IList`1" /> に挿入するオブジェクト。 </param>
        ///<exception cref="TValue:System.ArgumentOutOfRangeException">
        ///  <paramref name="n" />
        ///  が
        ///  <see cref="TValue:System.Collections.Generic.IList`1" />
        ///  の有効なインデックスではありません。</exception>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.IList`1" />
        ///  は読み取り専用です。</exception>
        public void Insert(int index, T item) {
            _list.Insert(index, item);
        }

        ///<summary>
        ///  指定したインデックス位置の <see cref="TValue:System.Collections.Generic.IList`1" /> 項目を削除します。
        ///</summary>
        ///<param name="n"> 削除する項目の 0 から始まるインデックス。 </param>
        ///<exception cref="TValue:System.ArgumentOutOfRangeException">
        ///  <paramref name="n" />
        ///  が
        ///  <see cref="TValue:System.Collections.Generic.IList`1" />
        ///  の有効なインデックスではありません。</exception>
        ///<exception cref="TValue:System.NotSupportedException">
        ///  <see cref="TValue:System.Collections.Generic.IList`1" />
        ///  は読み取り専用です。</exception>
        public void RemoveAt(int index) {
            _list.RemoveAt(index);
        }

        ///<summary>
        ///  指定したインデックスにある要素を取得または設定します。
        ///</summary>
        ///<returns> 指定したインデックスにある要素。 </returns>
        ///<param name="n"> 取得または設定する要素の、0 から始まるインデックス番号。 </param>
        ///<exception cref="TValue:System.ArgumentOutOfRangeException">
        ///  <paramref name="n" />
        ///  が
        ///  <see cref="TValue:System.Collections.Generic.IList`1" />
        ///  の有効なインデックスではありません。</exception>
        ///<exception cref="TValue:System.NotSupportedException">このプロパティが設定されていますが、
        ///  <see cref="TValue:System.Collections.Generic.IList`1" />
        ///  が読み取り専用です。</exception>
        public T this[int index] {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        ///<summary>
        ///  コレクションを反復処理する列挙子を返します。
        ///</summary>
        ///<returns> コレクションを反復処理するために使用できる <see cref="TValue:System.Collections.IEnumerator" /> オブジェクト。 </returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion
    }
}