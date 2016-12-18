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
        [TestCase(ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = 3)]
        [TestCase(1, 2, ExpectedResult = 4)]
        [TestCase(0, 1, 2, ExpectedResult = 2)]
        public int AggregateApartFirst(params int[] values) {
            try {
                return values.AggregateApartFirst(v => v + 1, (i, j) => i * j);
            } catch (InvalidOperationException) {
                return 0;
            }
        }

        [Test]
        [TestCase(ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = -3)]
        [TestCase(1, 2, ExpectedResult = -4)]
        [TestCase(0, 1, 2, ExpectedResult = -2)]
        public int AggregateApartFirst2(params int[] values) {
            try {
                return values.AggregateApartFirst(v => v + 1, (i, j) => i * j, v => -v);
            } catch (InvalidOperationException) {
                return 0;
            }
        }

        [Test]
        [TestCase(ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(1, 2, ExpectedResult = 1)]
        [TestCase(0, 1, 2, ExpectedResult = 1)]
        public int AggregateRight(params int[] values) {
            try {
                return values.AggregateReverse((i, j) => i - j);
            } catch (InvalidOperationException) {
                return 0;
            }
        }

        [Test]
        [TestCase(ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = -2)]
        [TestCase(1, 2, ExpectedResult = -3)]
        [TestCase(0, 1, 2, ExpectedResult = -3)]
        public int AggregateRight2(params int[] values) {
            return values.AggregateReverse(0, (i, j) => i - j);
        }

        [Test]
        [TestCase(ExpectedResult = new int[0])]
        [TestCase(1, ExpectedResult = new int[0])]
        [TestCase(1, 2, ExpectedResult = new[] { 1, 2 })]
        [TestCase(1, 2, 3, ExpectedResult = new[] { 1, 2 })]
        [TestCase(1, 2, 3, 4, ExpectedResult = new[] { 1, 2, 3, 4 })]
        public int[] Split2(params int[] values) {
            var results = new List<int>();
            foreach (var t in values.Split2()) {
                results.Add(t.Item1);
                results.Add(t.Item2);
            }
            return results.ToArray();
        }

        [Test]
        [TestCase(ExpectedResult = null)]
        [TestCase(1, ExpectedResult = new[] { 1, 1 })]
        [TestCase(1, 2, ExpectedResult = new[] { 1, 2 })]
        [TestCase(1, 2, 3, ExpectedResult = new[] { 1, 3 })]
        [TestCase(1, 2, 3, 4, ExpectedResult = new[] { 1, 4 })]
        public int[] FirstAndLast(params int[] values) {
            try {
                var tuple = values.FirstAndLast();
                return new[] { tuple.Item1, tuple.Item2 };
            } catch (InvalidOperationException) {
                return null;
            }
        }

        [Test]
        [TestCase(ExpectedResult = null)]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(1, 2, ExpectedResult = new[] { 2, 2 })]
        [TestCase(1, 2, 3, ExpectedResult = new[] { 2, 2 })]
        [TestCase(1, 2, 3, 4, ExpectedResult = new[] { 2, 4 })]
        public int[] FirstAndLastWithPredicate(params int[] values) {
            try {
                var tuple = values.FirstAndLast(i => i % 2 == 0);
                return new[] { tuple.Item1, tuple.Item2 };
            } catch (InvalidOperationException) {
                return null;
            }
        }

        [Test]
        [TestCase(ExpectedResult = null)]
        [TestCase(1, ExpectedResult = new[] { 1, 1 })]
        [TestCase(1, 2, ExpectedResult = new[] { 1, 2 })]
        [TestCase(1, 2, 3, ExpectedResult = new[] { 1, 3 })]
        [TestCase(1, 2, 3, 4, ExpectedResult = new[] { 1, 4 })]
        public int[] FirstAndLastOrNull(params int[] values) {
            var tuple = values.FirstAndLastOrNull();
            return tuple != null ? new[] { tuple.Item1, tuple.Item2 } : null;
        }

        [Test]
        [TestCase(ExpectedResult = null)]
        [TestCase(1, ExpectedResult = null)]
        [TestCase(1, 2, ExpectedResult = new[] { 2, 2 })]
        [TestCase(1, 2, 3, ExpectedResult = new[] { 2, 2 })]
        [TestCase(1, 2, 3, 4, ExpectedResult = new[] { 2, 4 })]
        public int[] FirstAndLastOrNullWithPredicate(params int[] values) {
            var tuple = values.FirstAndLastOrNull(i => i % 2 == 0);
            return tuple != null ? new[] { tuple.Item1, tuple.Item2 } : null;
        }

        [Test]
        [TestCase(ExpectedResult = new[] { 0, 0 })]
        [TestCase(1, ExpectedResult = new[] { 1, 1 })]
        [TestCase(1, 2, ExpectedResult = new[] { 1, 2 })]
        [TestCase(1, 2, 3, ExpectedResult = new[] { 1, 3 })]
        [TestCase(1, 2, 3, 4, ExpectedResult = new[] { 1, 4 })]
        public int[] FirstAndLastOrDefault(params int[] values) {
            var tuple = values.FirstAndLastOrDefault();
            return tuple != null ? new[] { tuple.Item1, tuple.Item2 } : null;
        }

        [Test]
        [TestCase(ExpectedResult = new[] { 0, 0 })]
        [TestCase(1, ExpectedResult = new[] { 0, 0 })]
        [TestCase(1, 2, ExpectedResult = new[] { 2, 2 })]
        [TestCase(1, 2, 3, ExpectedResult = new[] { 2, 2 })]
        [TestCase(1, 2, 3, 4, ExpectedResult = new[] { 2, 4 })]
        public int[] FirstAndLastOrDefaultWithPredicate(params int[] values) {
            var tuple = values.FirstAndLastOrDefault(i => i % 2 == 0);
            return tuple != null ? new[] { tuple.Item1, tuple.Item2 } : null;
        }
    }
}