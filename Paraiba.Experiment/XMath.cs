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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Paraiba {
	public static class XMath {
		public static bool InRange(int val, int smaller, int bigger) {
			Debug.Assert(smaller <= bigger);
			return smaller <= val && val <= bigger;
		}

		public static int Center(int val, int smaller, int bigger) {
			Debug.Assert(smaller <= bigger);
			if (val <= smaller) {
				return smaller;
			}
			if (val >= bigger) {
				return bigger;
			}
			return val;
		}

		public static int Max(int val1, int val2) {
			return val1 >= val2 ? val1 : val2;
		}

		public static int Max(params int[] values) {
			return values.Max();
		}

		public static int Max(IEnumerable<int> values) {
			return values.Max();
		}

		public static int Min(int val1, int val2) {
			return val1 <= val2 ? val1 : val2;
		}

		public static int Min(params int[] values) {
			return values.Min();
		}

		public static int Min(IEnumerable<int> values) {
			return values.Min();
		}

		public static int Gcd(int a, int b) {
			while (b != 0) {
				var c = a;
				a = b;
				b = c % b;
			}
			return a;
		}

		public static int Gcd(IEnumerable<int> values) {
			return values.Aggregate(Gcd);
		}

		public static int Gcd(params int[] values) {
			return Gcd((IEnumerable<int>)values);
		}

		public static BigInteger Gcd(BigInteger a, BigInteger b) {
			while (b != 0) {
				var c = a;
				a = b;
				b = c % b;
			}
			return a;
		}

		public static BigInteger Gcd(IEnumerable<BigInteger> values) {
			return values.Aggregate(Gcd);
		}

		public static BigInteger Gcd(params BigInteger[] values) {
			return Gcd((IEnumerable<BigInteger>)values);
		}
	}
}