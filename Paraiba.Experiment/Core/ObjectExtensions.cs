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

namespace Paraiba.Core {
    public static class ObjectExtensions {
        /// <summary>
        ///   nullでないとき実行
        /// </summary>
        /// <typeparam name="TValue"> </typeparam>
        /// <param name="arg"> this </param>
        /// <param name="action"> 行うアクション </param>
        public static void NotNull<T>(this T arg, Action<T> action) {
            if (arg != null) {
                action(arg);
            }
        }
    }
}