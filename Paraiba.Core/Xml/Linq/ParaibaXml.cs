#region License

// Copyright (C) 2012 Kazunori Sakamoto
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
using System.Xml.Linq;

namespace Paraiba.Xml.Linq {
	/// <summary>
	///   A utility class for <see cref="XElement" /> .
	/// </summary>
	public static class ParaibaXml {
		/// <summary>
		///   Returns a value whether <see cref="XElement" /> values are equal using descendant elements and values.
		/// </summary>
		/// <param name="element1"> One of comparing elements. </param>
		/// <param name="element2"> Another comparing element. </param>
		/// <returns> The value whether <see cref="XElement" /> values are equal using descendant elements and values. </returns>
		public static bool EqualsElementsAndValues(
				XElement element1, XElement element2) {
			Contract.Requires(element1 != null);
			Contract.Requires(element2 != null);

			if (!element1.Name.Equals(element2.Name)) {
				return false;
			}

			var hasElements = element1.HasElements;
			if (hasElements != element2.HasElements) {
				return false;
			}

			if (!hasElements) {
				return element1.Value == element2.Value;
			}

			using (var itr1 = element1.Elements().GetEnumerator()) {
				using (var itr2 = element2.Elements().GetEnumerator()) {
					while (itr1.MoveNext()) {
						if (!itr2.MoveNext()) {
							return false;
						}
						if (!EqualsElementsAndValues(itr1.Current, itr2.Current)) {
							return false;
						}
					}
				}
			}
			return true;
		}
	}
}