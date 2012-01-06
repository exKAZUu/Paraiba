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

namespace Paraiba.Wrap {
    // 反変性のためインタフェースが必要
    public abstract class Wrap<T> : IWrap<T> {
        #region IWrap<T> Members

        public abstract T Value { get; }

        #endregion

        // TValue -> Wrap<TValue> への暗黙な変換はバグの原因になる可能性大？
        public static implicit operator Wrap<T>(T value) {
            return new ValueWrap<T>(value);
        }

        public static implicit operator Wrap<T>(Func<T> evaluator) {
            return new LazyWrap<T>(evaluator);
        }

        public static implicit operator T(Wrap<T> wrap) {
            return wrap.Value;
        }
    }
}