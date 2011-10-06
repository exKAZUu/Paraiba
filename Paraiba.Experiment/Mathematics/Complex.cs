using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Paraiba.Mathematics
{
	// http://d.hatena.ne.jp/NyaRuRu/20060802
	public struct Number<T>
		where T : struct
	{
		public static readonly Func<Number<T>, Number<T>, T> Add;

		static Number()
		{
			var mi = typeof(T).GetMethod("op_Addition", BindingFlags.Static | BindingFlags.Public);
			var p1 = Expression.Parameter(typeof(Number<T>), "a");
			var p2 = Expression.Parameter(typeof(Number<T>), "b");
			var p1Value = Expression.Property(p1, "Value");
			var p2Value = Expression.Property(p2, "Value");
			Expression body;
			if (mi != null)
			{
				body = Expression.Call(mi, p1Value, p2Value);
			}
			else
			{
				body = Expression.Add(p1Value, p2Value);
			}
			var lambda = Expression.Lambda<Func<Number<T>, Number<T>, T>>(body, p1, p2);
			Add = lambda.Compile();
		}

		private readonly T _value;

		public T Value
		{
			get
			{
				return _value;
			}
		}

		public Number(T value)
		{
			_value = value;
		}

		public static Number<T> operator +(Number<T> a, Number<T> b)
		{
			return new Number<T>(Add(a, b));
		}

		public static implicit operator Number<T>(T a)
		{
			return new Number<T>(a);
		}

		public static implicit operator T(Number<T> a)
		{
			return a.Value;
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}

	public struct Complex<T> where T : struct
	{
		public readonly Number<T> Real;
		public readonly Number<T> Imaginal;

		public Complex(T r, T i)
		{
			Real = r;
			Imaginal = i;
		}

		public static Complex<T> operator +(Complex<T> a, Complex<T> b)
		{
			return new Complex<T>(a.Real + b.Real, a.Imaginal + b.Imaginal);
		}

		public override string ToString()
		{
			return string.Format("{0}+{1}i", Real, Imaginal);
		}
	}
}