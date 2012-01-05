using System;

namespace Paraiba.Wrap {
	public class ReadonlyFuncWrap<T> : Wrap<T>
	{
		public ReadonlyFuncWrap(Func<T> func)
		{
			Func = func;
		}

		public Func<T> Func { get; private set; }

		public override T Value
		{
			get { return Func(); }
		}
	}
}