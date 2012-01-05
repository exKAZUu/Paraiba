using System;

namespace Paraiba.Wrap {
	public class LazyFlagWrap<T> : Wrap<T>
	{
		private T _value;

		public LazyFlagWrap(Func<T> evaluator)
		{
			Evaluator = evaluator;
		}

		public bool Evaluated { get; set; }

		public Func<T> Evaluator { get; set; }

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