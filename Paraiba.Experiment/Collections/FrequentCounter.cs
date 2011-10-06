using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Paraiba.Collections
{
	public class FrequentCounter : IEnumerable<int>
	{
		private readonly int[] _frequencies;
		private readonly int _startSymbol;

		public int this[int symbol]
		{
			get
			{
				return _frequencies[symbol - _startSymbol];
			}
			set
			{
				_frequencies[symbol - _startSymbol] = value;
			}
		}

		public IEnumerable<int> Ascendings
		{
			get
			{
				for (int i = 0; i < _frequencies.Length; i++)
				{
					var count = _frequencies[i];
					while (--count >= 0)
					{
						yield return i + _startSymbol;
					}
				}
			}
		}

		public IEnumerable<int> Descendings
		{
			get
			{
				for (int i = _frequencies.Length - 1; i >= 0; i--)
				{
					var count = _frequencies[i];
					while (--count >= 0)
					{
						yield return i + _startSymbol;
					}
				}
			}
		}

		public FrequentCounter(int count)
			: this(0, count)
		{
			Contract.Requires(0 <= count);
		}

		public FrequentCounter(int startSymbol, int count)
		{
			Contract.Requires(0 <= count);
			Contract.Requires(0 <= startSymbol);
			Contract.Requires(startSymbol < count);

			_frequencies = new int[count];
			_startSymbol = startSymbol;
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_frequencies != null);
		}

		public IEnumerator<int> GetEnumerator()
		{
			return Ascendings.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
