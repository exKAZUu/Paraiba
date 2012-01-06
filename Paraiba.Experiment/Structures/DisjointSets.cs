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
    /// <summary>
    ///   多くのデータを互いに素な集合(1つの要素が複数の集合に属することがない集合)に分類して管理するためのデータ構造 Union-Findアルゴリズムに用いられる http://algorithms.blog55.fc2.com/blog-entry-46.html
    /// </summary>
    /// <author>wirbelwind</author>
    public class DisjointSets {
        private readonly int[] _parent;
        private readonly int[] _rank;

        public DisjointSets(int size) {
            _parent = new int[size];
            _rank = new int[size];
        }

        /// <summary>
        ///   指定された値を持つ集合を作成
        /// </summary>
        /// <param name="value"> 作成する集合の要素 </param>
        public void MakeSet(int value) {
            _parent[value] = value;
            _rank[value] = 0;
        }

        /// <summary>
        ///   指定された値の集合を求める
        /// </summary>
        /// <param name="x"> 求めたい集合の要素 </param>
        /// <returns> 集合の要素の代表値 </returns>
        public int FindSet(int x) {
            if (x != _parent[x]) {
                _parent[x] = FindSet(_parent[x]);
            }
            return _parent[x];
        }

        private void Link(int x, int y) {
            if (_rank[x] > _rank[y]) {
                _parent[y] = x;
            } else {
                _parent[x] = y;
                if (_rank[x] == _rank[y]) {
                    _rank[y]++;
                }
            }
        }

        /// <summary>
        ///   2つの集合を統合する
        /// </summary>
        /// <param name="x"> 1つめの集合の要素 </param>
        /// <param name="y"> 2つめの集合の要素 </param>
        public void Union(int x, int y) {
            Link(FindSet(x), FindSet(y));
        }

        /// <summary>
        ///   2つの値が同じ集合に属するかどうか調べる
        /// </summary>
        /// <param name="x"> 調べたい値 </param>
        /// <param name="y"> 調べたい値 </param>
        /// <returns> </returns>
        private bool IsSameSet(int x, int y) {
            return FindSet(x) == FindSet(y);
        }
    }
}