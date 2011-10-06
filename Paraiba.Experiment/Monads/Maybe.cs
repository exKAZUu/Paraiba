using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paraiba.Monads
{
	public static class MaybeMoand
	{
		public static Maybe<T> Return<T>(T value)
		{
			return new Just<T>(value);
		}
	}

	public abstract class Maybe<T>
	{
		public abstract bool HasValue
		{
			get;
		}

		public abstract T Value
		{
			get;
		}

		public abstract Maybe<T2> Bind<T2>(Func<T, Maybe<T2>> func);

		public static Maybe<T> Return(T value)
		{
			return new Just<T>(value);
		}
	}

	public class Nothing<T> : Maybe<T>
	{
		private static readonly Nothing<T> This = new Nothing<T>();

		public override bool HasValue
		{
			get { return false; }
		}

		public override T Value
		{
			get { return default(T); }
		}

		public override Maybe<T2> Bind<T2>(Func<T, Maybe<T2>> func)
		{
			return Nothing<T2>.This;
		}
	}

	public class Just<T> : Maybe<T>
	{
		private readonly T _value;

		public override bool HasValue
		{
			get { return true; }
		}

		public override T Value
		{
			get { return _value; }
		}

		public Just(T value)
		{
			_value = value;
		}

		public override Maybe<T2> Bind<T2>(Func<T, Maybe<T2>> func)
		{
			return func(_value);
		}
	}
}
