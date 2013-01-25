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
using System.Linq;
using NUnit.Framework;
using Paraiba.Collections.Generic;

namespace Paraiba.Tests.Collections.Generic {
	[TestFixture]
	public class ListExtensionsTest {
		[Test]
		[TestCase(new[] { 1, 2 }, 5, 3, Result = new[] { 1, 2, 3, 3, 3 })]
		[TestCase(new[] { 1, 2 }, 1, 3, Result = new[] { 1, 2 })]
		public int[] Extend(int[] list, int count, int defaultValue) {
			return list.ToList().Extend(count, defaultValue).ToArray();
		}

		[Test]
		public void Shuffle() {
			var rand1 = new Random(0);
			var rand2 = new Random(1);
			var ints1 = Enumerable.Range(0, 1000).ToArray();
			var ints2 = Enumerable.Range(0, 1000).ToArray();
			Assert.That(ints1.Shuffle(rand1), Is.Not.EqualTo(ints2.Shuffle(rand2)));
		}

		[Test]
		public void ShuffleUsingSameRandom() {
			var rand1 = new Random(0);
			var rand2 = new Random(0);
			var ints1 = Enumerable.Range(0, 1000).ToArray();
			var ints2 = Enumerable.Range(0, 1000).ToArray();
			Assert.That(ints1.Shuffle(rand1), Is.EqualTo(ints2.Shuffle(rand2)));
		}
	}
}