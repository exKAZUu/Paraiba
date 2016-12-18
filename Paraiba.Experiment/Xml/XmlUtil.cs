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

using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Linq;

namespace Paraiba.Xml {
    public static class XmlUtil {
        public static bool EqualsWithElementAndValue(XElement xe1, XElement xe2) {
            Contract.Requires(xe1 != null);
            Contract.Requires(xe2 != null);

            if (!xe1.Name.Equals(xe2.Name)) {
                return false;
            }

            var hasElements = xe1.HasElements;
            if (hasElements != xe2.HasElements) {
                return false;
            }

            if (!hasElements) {
                return xe1.Value == xe2.Value;
            }

            var es1 = xe1.Elements().ToList();
            var es2 = xe2.Elements().ToList();
            var count = es1.Count;
            if (count != es2.Count) {
                return false;
            }

            for (int i = 0; i < count; i++) {
                if (!EqualsWithElementAndValue(es1[i], es2[i])) {
                    return false;
                }
            }
            return true;
        }
    }
}