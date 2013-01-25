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

namespace Paraiba.Collections.Generic.Compare {
	public class NonGenericComparer<T> : IComparer, IComparer<T> {
		private readonly IComparer<T> _comparer;

		public NonGenericComparer(IComparer<T> comparer) {
			_comparer = comparer;
		}

		public IComparer<T> Comparer {
			get { return _comparer; }
		}

		#region IComparer Members

		public int Compare(object x, object y) {
			if (x is T) {
				return y is T ? _comparer.Compare((T)x, (T)y) : -1;
			}
			return y is T ? 1 : (x.Equals(y) ? 0 : -1);
		}

		#endregion

		#region IComparer<T> Members

		public int Compare(T x, T y) {
			return _comparer.Compare(x, y);
		}

		#endregion
	}
}