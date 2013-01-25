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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Paraiba.Collections {
	public class FrequentCounter : IEnumerable<int> {
		private readonly int[] _frequencies;
		private readonly int _startSymbol;

		public FrequentCounter(int count)
				: this(0, count) {
			Contract.Requires(0 <= count);
		}

		public FrequentCounter(int startSymbol, int count) {
			Contract.Requires(0 <= count);
			Contract.Requires(0 <= startSymbol);
			Contract.Requires(startSymbol < count);

			_frequencies = new int[count];
			_startSymbol = startSymbol;
		}

		public int this[int symbol] {
			get { return _frequencies[symbol - _startSymbol]; }
			set { _frequencies[symbol - _startSymbol] = value; }
		}

		public IEnumerable<int> Ascendings {
			get {
				for (int i = 0; i < _frequencies.Length; i++) {
					var count = _frequencies[i];
					while (--count >= 0) {
						yield return i + _startSymbol;
					}
				}
			}
		}

		public IEnumerable<int> Descendings {
			get {
				for (int i = _frequencies.Length - 1; i >= 0; i--) {
					var count = _frequencies[i];
					while (--count >= 0) {
						yield return i + _startSymbol;
					}
				}
			}
		}

		#region IEnumerable<int> Members

		public IEnumerator<int> GetEnumerator() {
			return Ascendings.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		#endregion

		[ContractInvariantMethod]
		private void ObjectInvariant() {
			Contract.Invariant(_frequencies != null);
		}
	}
}