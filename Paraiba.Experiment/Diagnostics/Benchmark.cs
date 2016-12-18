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

using System;
using System.Diagnostics;

namespace Paraiba.Diagnostics {
    public class Benchmark : IDisposable {
        private static int count = 1;
        private static long sumTime;

        private readonly string _name;
        private readonly Stopwatch _watch;

        public Benchmark() {
            _name = null;
            _watch = new Stopwatch();
            _watch.Start();
        }

        public Benchmark(string name) {
            _name = name;
            _watch = new Stopwatch();
            _watch.Start();
        }

        #region IDisposable Members

        ///<summary>
        ///  アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose() {
            _watch.Stop();
            var time = _watch.ElapsedTicks;
            sumTime += time;

            if (_name != null) {
                Console.WriteLine(
                    "[" + count++ + "] " + _name + " : " + time + " [ms], "
                    + sumTime + " [ms]");
            } else {
                Console.WriteLine(
                    "[" + count++ + "] " + time + " [ms], " + sumTime
                    + " [ms]");
            }
        }

        #endregion
    }
}