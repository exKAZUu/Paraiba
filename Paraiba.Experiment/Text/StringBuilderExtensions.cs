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

namespace Paraiba.Text {
    public static class StringBuilderExtensions {
        public static StringBuilder Appends<T>(
                this StringBuilder builder, IEnumerable<T> values) {
            foreach (var value in values) {
                builder.Append(value);
            }
            return builder;
        }

        public static StringBuilder AppendLines<T>(
                this StringBuilder builder, IEnumerable<T> values) {
            foreach (var value in values) {
                builder.AppendLine(value.ToString());
            }
            return builder;
        }

        public static StringBuilder JoinStringBuilder<T>(
                this IEnumerable<T> strs) {
            using (var enumerator = strs.GetEnumerator()) {
                if (enumerator.MoveNext()) {
                    var builder =
                            new StringBuilder(enumerator.Current.ToString());
                    while (enumerator.MoveNext()) {
                        builder.Append(enumerator.Current);
                    }
                    return builder;
                }
            }
            return new StringBuilder();
        }

        public static StringBuilder JoinStringBuilder<T>(
                this IEnumerable<T> strs, string delimiter) {
            using (var enumerator = strs.GetEnumerator()) {
                if (enumerator.MoveNext()) {
                    var builder =
                            new StringBuilder(enumerator.Current.ToString());
                    while (enumerator.MoveNext()) {
                        builder.Append(delimiter);
                        builder.Append(enumerator.Current);
                    }
                    return builder;
                }
            }
            return new StringBuilder();
        }
    }
}