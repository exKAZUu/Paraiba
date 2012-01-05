using NUnit.Framework;
using Paraiba.Linq;

namespace Paraiba.Core.Tests.Linq {
    [TestFixture]
    public class EnumerableExtensionsTest {
        [Test]
        [TestCase(new[] {2}, Result = 3)]
        [TestCase(new[] {1, 2}, Result = 4)]
        [TestCase(new[] {0, 1, 2}, Result = 2)]
        public int AggregateApartFirst(int[] values) {
            return values.AggregateApartFirst(v => v + 1, (i, j) => i * j);
        }

        [Test]
        [TestCase(new[] {2}, Result = -3)]
        [TestCase(new[] {1, 2}, Result = -4)]
        [TestCase(new[] {0, 1, 2}, Result = -2)]
        public int AggregateApartFirst2(int[] values) {
            return values.AggregateApartFirst(v => v + 1, (i, j) => i * j, v => -v);
        }
    }
}