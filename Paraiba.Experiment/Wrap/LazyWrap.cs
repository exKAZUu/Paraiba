using System;

namespace Paraiba.Wrap {
	public class LazyWrap<T> : Wrap<T>
	{
		private T _value;

		public LazyWrap(Func<T> evaluator)
		{
			Evaluator = evaluator;
		}

		public bool Evaluated
		{
			get { return Evaluator == null; }
		}

		public Func<T> Evaluator { get; set; }

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