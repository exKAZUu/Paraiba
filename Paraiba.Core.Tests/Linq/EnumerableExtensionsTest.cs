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

using NUnit.Framework;
using Paraiba.Linq;

namespace Paraiba.Core.Tests.Linq {
    [TestFixture]
    public class EnumerableExtensionsTest {
        [Test]
        [TestCase(new[] { 2 }, Result = 3)]
        [TestCase(new[] { 1, 2 }, Result = 4)]
        [TestCase(new[] { 0, 1, 2 }, Result = 2)]
        public int AggregateApartFirst(int[] values) {
            return values.AggregateApartFirst(v => v + 1, (i, j) => i * j);
        }

        [Test]
        [TestCase(new[] { 2 }, Result = -3)]
        [TestCase(new[] { 1, 2 }, Result = -4)]
        [TestCase(new[] { 0, 1, 2 }, Result = -2)]
        public int AggregateApartFirst2(int[] values) {
            return values.AggregateApartFirst(
                    v => v + 1, (i, j) => i * j, v => -v);
        }
    }
}