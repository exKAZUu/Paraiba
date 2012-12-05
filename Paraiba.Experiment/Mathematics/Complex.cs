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
using System.Linq.Expressions;
using System.Reflection;

namespace Paraiba.Mathematics {
	// http://d.hatena.ne.jp/NyaRuRu/20060802
	public struct Number<T>
			where T : struct {
		public static readonly Func<Number<T>, Number<T>, T> Add;

		private readonly T _value;

		static Number() {
			var mi = typeof(T).GetMethod(
					"op_Addition", BindingFlags.Static | BindingFlags.Public);
			var p1 = Expression.Parameter(typeof(Number<T>), "a");
			var p2 = Expression.Parameter(typeof(Number<T>), "b");
			var p1Value = Expression.Property(p1, "Value");
			var p2Value = Expression.Property(p2, "Value");
			Expression body;
			if (mi != null) {
				body = Expression.Call(mi, p1Value, p2Value);
			} else {
				body = Expression.Add(p1Value, p2Value);
			}
			var lambda = Expression.Lambda<Func<Number<T>, Number<T>, T>>(
					body, p1, p2);
			Add = lambda.Compile();
		}

		public Number(T value) {
			_value = value;
		}

		public T Value {
			get { return _value; }
		}

		public static Number<T> operator +(Number<T> a, Number<T> b) {
			return new Number<T>(Add(a, b));
		}

		public static implicit operator Number<T>(T a) {
			return new Number<T>(a);
		}

		public static implicit operator T(Number<T> a) {
			return a.Value;
		}

		public override string ToString() {
			return _value.ToString();
		}
	}

	public struct Complex<T>
			where T : struct {
		public readonly Number<T> Imaginal;
		public readonly Number<T> Real;

		public Complex(T r, T i) {
			Real = r;
			Imaginal = i;
		}

		public static Complex<T> operator +(Complex<T> a, Complex<T> b) {
			return new Complex<T>(a.Real + b.Real, a.Imaginal + b.Imaginal);
		}

		public override string ToString() {
			return string.Format("{0}+{1}i", Real, Imaginal);
		}
	}
}