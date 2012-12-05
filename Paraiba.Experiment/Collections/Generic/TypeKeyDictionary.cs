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
using System.Collections;
using System.Collections.Generic;

namespace Paraiba.Collections.Generic {
    public class TypeKeyDictionary<TValue> : IEnumerable<TValue> {
        private readonly IDictionary<Type, TValue> _dic;

        public TypeKeyDictionary() {
            _dic = new Dictionary<Type, TValue>();
        }

        public int Count {
            get { return _dic.Count; }
        }

        #region IEnumerable<TValue> Members

        public IEnumerator<TValue> GetEnumerator() {
            return _dic.Values.GetEnumerator();
        }

        /// <summary>
        ///   コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns> コレクションを反復処理するために使用できる <see cref="TValue:System.Collections.IEnumerator" /> オブジェクト。 </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        public void Add<T>(T item)
                where T : TValue {
            _dic.Add(typeof(T), item);
        }

        public void Set<T>(T item)
                where T : TValue {
            _dic[typeof(T)] = item;
        }

        public void Clear() {
            _dic.Clear();
        }

        public bool Contains<T>()
                where T : TValue {
            return _dic.ContainsKey(typeof(T));
        }

        public bool Contains(Type type) {
            return _dic.ContainsKey(type);
        }

        public bool Remove<T>()
                where T : TValue {
            return _dic.Remove(typeof(T));
        }

        public bool Remove(Type type) {
            return _dic.Remove(type);
        }

        public T Get<T>()
                where T : TValue {
            return (T)_dic[typeof(T)];
        }

        public TValue Get(Type type) {
            return _dic[type];
        }

        public bool TryGetValue<T>(out T value)
                where T : TValue {
            TValue tmp;
            var result = _dic.TryGetValue(typeof(T), out tmp);
            value = (T)tmp;
            return result;
        }

        public bool TryGetValue<T>(Type type, out TValue value) {
            return _dic.TryGetValue(type, out value);
        }

        public T GetValueOrDefault<T>()
                where T : TValue {
            TValue tmp;
            if (!_dic.TryGetValue(typeof(T), out tmp)) {
                return default(T);
            }
            return (T)tmp;
        }

        public TValue GetValueOrDefault(Type type) {
            TValue tmp;
            if (!_dic.TryGetValue(type, out tmp)) {
                return default(TValue);
            }
            return tmp;
        }
    }
}