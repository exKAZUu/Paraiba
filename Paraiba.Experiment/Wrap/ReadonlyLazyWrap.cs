using System;

namespace Paraiba.Wrap {
	public class ReadonlyLazyWrap<T> : Wrap<T>
	{
		private T _value;

		public ReadonlyLazyWrap(Func<T> evaluator)
		{
			Evaluator = evaluator;
		}

		public bool Evaluated
		{
			get { return Evaluator == null; }
		}

		public Func<T> Evaluator { get; private set; }

		public override T Value
		{
			get
			{
				if (Evaluator != null)
				{
					_value = Evaluator();
					Evaluator = null;
				}
				return _value;
			}
		}
	}
}