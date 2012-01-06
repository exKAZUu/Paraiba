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

namespace Paraiba.Utility {
    public class MappedInputer<TKey> {
        private readonly Dictionary<TKey, KeyData> _map =
                new Dictionary<TKey, KeyData>();

        public void Add(TKey key, Func<bool> isPressFunc) {
            _map[key] = new KeyData(isPressFunc);
        }

        public void Clear() {
            _map.Clear();
        }

        public void Update() {
            foreach (var keyData in _map.Values) {
                keyData.Old = keyData.Now;
                keyData.Now = keyData.IsPressFunc();
            }
        }

        public bool IsPush(TKey key) {
            var keyData = _map[key];
            return (keyData.Now & !keyData.Old);
        }

        public bool IsPress(TKey key) {
            return _map[key].Now;
        }

        public bool IsPressNow(TKey key) {
            return _map[key].IsPressFunc();
        }

        public bool IsRelease(TKey key) {
            var keyData = _map[key];
            return (!keyData.Now & keyData.Old);
        }

        #region Nested type: KeyData

        private class KeyData {
            internal readonly Func<bool> IsPressFunc;
            internal bool Now, Old;

            internal KeyData(Func<bool> isPressFunc) {
                IsPressFunc = isPressFunc;
                Now = false;
                Old = false;
            }
        }

        #endregion
    }
}