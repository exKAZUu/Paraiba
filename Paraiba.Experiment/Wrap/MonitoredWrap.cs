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

namespace Paraiba.Wrap {
    public class MonitoredWrap<T> : MutableWrap<T> {
        private readonly IEqualityComparer<T> _cmp;
        private T _value;

        public MonitoredWrap(T value)
                : this(value, EqualityComparer<T>.Default) {}

        public MonitoredWrap(T value, IEqualityComparer<T> cmp) {
            _value = value;
            _cmp = cmp;
        }

        public Func<T, T, T> Changing { get; set; }
        public Action<T, T> Changed { get; set; }

        public override T Value {
            get { return _value; }
        }

        public override void Set(T value) {
            var old = _value;
            if (!_cmp.Equals(old, value) && Changing != null) {
                value = Changing(value, old);
            }
            _value = value;
            if (!_cmp.Equals(old, value) && Changed != null) {
                Changed(value, old);
            }
        }
    }

    public static class MonitoredWrap {
        public static MonitoredWrap<T> Create<T>(T value) {
            return new MonitoredWrap<T>(value);
        }
    }
}