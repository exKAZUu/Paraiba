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
using System.Collections.Generic;

namespace Paraiba.Collections.Generic.Compare {
	public class PartialComparer<TSource, TKey> : IComparer<TSource>
			where TKey : IComparable<TKey> {
		private readonly Func<TSource, TKey> _selectFunc;

		public PartialComparer(Func<TSource, TKey> selectFunc) {
			_selectFunc = selectFunc;
		}

		public Func<TSource, TKey> SelectFunc {
			get { return _selectFunc; }
		}

		#region IComparer<TSource> Members

		public int Compare(TSource x, TSource y) {
			return _selectFunc(x).CompareTo(_selectFunc(y));
		}

		#endregion
	}
}