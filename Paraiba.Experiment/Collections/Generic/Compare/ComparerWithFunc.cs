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
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic.Compare {
    public class ComparerWithFunc<T> : IComparer<T> {
        private readonly Func<T, T, int> _compareFunc;

        public ComparerWithFunc(Func<T, T, int> compareFunc) {
            Contract.Requires(compareFunc != null);
            _compareFunc = compareFunc;
        }

        #region IComparer<TValue> メンバ

        public int Compare(T x, T y) {
            return _compareFunc(x, y);
        }

        #endregion

        public Func<T, T, int> CompareFunc {
            get { return _compareFunc; }
        }
    }
}