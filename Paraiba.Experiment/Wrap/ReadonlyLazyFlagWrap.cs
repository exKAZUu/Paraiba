using System;

namespace Paraiba.Utility {
	public class ReadonlyLazyFlagWrap<T> : Wrap<T>
	{
		private T _value;

		public ReadonlyLazyFlagWrap(Func<T> evaluator)
		{
			Evaluator = evaluator;
		}

		public bool Evaluated { get; set; }

		public Func<T> Evaluator { get; private set; }

		public override T Value
		{
			get
			{
				if (Evaluated == false)
				{
					_value = Evaluator();
					Evaluated = true;
				}
				return _value;
			}
		}
	}
}