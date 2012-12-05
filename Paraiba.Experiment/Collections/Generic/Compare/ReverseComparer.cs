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
using System.Diagnostics.Contracts;

namespace Paraiba.Collections.Generic.Compare {
    public class ReverseComparer<T> : IComparer<T> {
        private readonly IComparer<T> _cmp;

        public ReverseComparer(IComparer<T> cmp) {
            Contract.Requires(cmp != null);

            _cmp = cmp;
        }

        public IComparer<T> OriginalComparer {
            get { return _cmp; }
            //set { _cmp = value; }	// 状態変化できない方が良いかな
        }

        #region IComparer<T> Members

        public int Compare(T x, T y) {
            return _cmp.Compare(x, y) * -1;
        }

        #endregion
    }
}