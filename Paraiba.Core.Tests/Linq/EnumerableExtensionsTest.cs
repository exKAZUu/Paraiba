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

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Paraiba.Linq;

namespace Paraiba.Tests.Linq {
    /// <summary>
    ///   Tests for <see cref="EnumerableExtensions" /> .
    /// </summary>
    [TestFixture]
    public class EnumerableExtensionsTest {
        [Test]
        [TestCase(new int[0], ExpectedException = typeof(InvalidOperationException))]
        [TestCase(new[] { 2 }, Result = 3)]
        [TestCase(new[] { 1, 2 }, Result = 4)]
        [TestCase(new[] { 0, 1, 2 }, Result = 2)]
        public int AggregateApartFirst(int[] values) {
            return values.AggregateApartFirst(v => v + 1, (i, j) => i * j);
        }

        [Test]
        [TestCase(new int[0], ExpectedException = typeof(InvalidOperationException))]
        [TestCase(new[] { 2 }, Result = -3)]
        [TestCase(new[] { 1, 2 }, Result = -4)]
        [TestCase(new[] { 0, 1, 2 }, Result = -2)]
        public int AggregateApartFirst2(int[] values) {
            return values.AggregateApartFirst(
                    v => v + 1, (i, j) => i * j, v => -v);
        }

        [Test]
        [TestCase(new int[0], ExpectedException = typeof(InvalidOperationException))]
        [TestCase(new[] { 2 }, Result = 2)]
        [TestCase(new[] { 1, 2 }, Result = 1)]
        [TestCase(new[] { 0, 1, 2 }, Result = 1)]
        public int AggregateRight(int[] values) {
            return values.AggregateReverse((i, j) => i - j);
        }

        [Test]
        [TestCase(new int[0], Result = 0)]
        [TestCase(new[] { 2 }, Result = -2)]
        [TestCase(new[] { 1, 2 }, Result = -3)]
        [TestCase(new[] { 0, 1, 2 }, Result = -3)]
        public int AggregateRight2(int[] values) {
            return values.AggregateReverse(0, (i, j) => i - j);
        }

        [Test]
        [TestCase(new int[0], Result = new int[0])]
        [TestCase(new[] { 1 }, Result = new int[0])]
        [TestCase(new[] { 1, 2 }, Result = new[] { 1, 2 })]
        [TestCase(new[] { 1, 2, 3 }, Result = new[] { 1, 2 })]
        [TestCase(new[] { 1, 2, 3, 4 }, Result = new[] { 1, 2, 3, 4 })]
        public int[] Split2(int[] values) {
            var result = new List<int>();
            foreach (var t in values.Split2()) {
                result.Add(t.Item1);
                result.Add(t.Item2);
            }
            return result.ToArray();
        }
    }
}