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
    public class MutableLazyFlagWrap<T> : MutableWrap<T> {
        private T _value;

        public MutableLazyFlagWrap(Func<T> evaluator) {
            Evaluator = evaluator;
        }

        public MutableLazyFlagWrap(T value, Func<T> evaluator, bool evaluated) {
            Evaluated = evaluated;
            Evaluator = evaluator;
            _value = value;
        }

        public bool Evaluated { get; set; }

        public Func<T> Evaluator { get; set; }

        public override T Value {
            get {
                if (Evaluated == false) {
                    _value = Evaluator();
                    Evaluated = true;
                }
                return _value;
            }
        }

        public override void Set(T value) {
            _value = value;
            Evaluated = true;
        }
    }
}