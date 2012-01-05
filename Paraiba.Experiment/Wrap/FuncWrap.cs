using System;

namespace Paraiba.Wrap {
	public class FuncWrap<T> : Wrap<T>
	{
		public FuncWrap(Func<T> func)
		{
			Func = func;
		}

		public Func<T> Func { get; set; }

		public override T Value
		{
			get { return Func(); }
		}
	}
}