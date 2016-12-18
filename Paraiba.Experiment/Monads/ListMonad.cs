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
using System.Collections.Generic;
using System.Linq;
using Paraiba.Linq;

namespace Paraiba.Monads {
    public static class ListMonad {
        public static IEnumerable<T2> Bind<T1, T2>(
            this IEnumerable<T1> source, Func<T1, IEnumerable<T2>> func) {
            return source.SelectMany(func);
        }

        public static IEnumerable<T> Return<T>(T value) {
            return value.ToEnumerable();
        }
    }
}