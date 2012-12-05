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

using System.Collections.Generic;
using System.Text;
using Paraiba.Text;

namespace Paraiba.Core {
    public static class CharExtensions {
        public static int ToInt(this char ch) {
            return ch - '0';
        }

        public static string JoinString(this IEnumerable<char> chars) {
            return JoinStringBuilder(chars).ToString();
        }

        public static StringBuilder JoinStringBuilder(
                this IEnumerable<char> chars) {
            var buffer = new StringBuilder();
            return buffer.Appends(chars);
        }

        public static string JoinString(this ICollection<char> chars) {
            return JoinStringBuilder(chars).ToString();
        }

        public static StringBuilder JoinStringBuilder(
                this ICollection<char> chars) {
            var buffer = new StringBuilder(chars.Count);
            return buffer.Appends(chars);
        }
    }
}