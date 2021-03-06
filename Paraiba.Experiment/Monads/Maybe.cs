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

using System;

namespace Paraiba.Monads {
    public static class MaybeMoand {
        public static Maybe<T> Return<T>(T value) {
            return new Just<T>(value);
        }
    }

    public abstract class Maybe<T> {
        public abstract bool HasValue { get; }

        public abstract T Value { get; }

        public abstract Maybe<T2> Bind<T2>(Func<T, Maybe<T2>> func);

        public static Maybe<T> Return(T value) {
            return new Just<T>(value);
        }
    }

    public class Nothing<T> : Maybe<T> {
        private static readonly Nothing<T> This = new Nothing<T>();

        public override bool HasValue {
            get { return false; }
        }

        public override T Value {
            get { return default(T); }
        }

        public override Maybe<T2> Bind<T2>(Func<T, Maybe<T2>> func) {
            return Nothing<T2>.This;
        }
    }

    public class Just<T> : Maybe<T> {
        private readonly T _value;

        public Just(T value) {
            _value = value;
        }

        public override bool HasValue {
            get { return true; }
        }

        public override T Value {
            get { return _value; }
        }

        public override Maybe<T2> Bind<T2>(Func<T, Maybe<T2>> func) {
            return func(_value);
        }
    }
}