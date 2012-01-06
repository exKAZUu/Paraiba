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

namespace Paraiba.Structures {
    public class BinaryIndexedTree {
        private readonly int[] _frequencyTree;

        public BinaryIndexedTree(int size) {
            _frequencyTree = new int[size];
        }

        public int sum(int from, int to) {
            if (from == 0) {
                int s = 0;
                for (int i = to; i >= 0; i = (i & (i + 1)) - 1) {
                    s += _frequencyTree[i];
                }
                return s;
            }

            return sum(0, to) - sum(0, from - 1);
        }

        public void add(int place, int incVal) {
            for (int i = place; i < _frequencyTree.Length; i |= i + 1) {
                _frequencyTree[i] += incVal;
            }
        }
    }
}