using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Collections.Generic
{
    public class EnumerableUnit<T> : IEnumerable<T>
    {
        private readonly T _unit;

        public EnumerableUnit(T unit) {
            _unit = unit;
        }

        public IEnumerator<T> GetEnumerator() {
            yield return _unit;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
