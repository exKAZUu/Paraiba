using System;

namespace Paraiba.Utility {
	public class MutableLazyFlagWrap<T> : MutableWrap<T>
	{
		private T _value;

		public MutableLazyFlagWrap(Func<T> evaluator)
		{
			Evaluator = evaluator;
		}

		public MutableLazyFlagWrap(T value, Func<T> evaluator, bool evaluated)
		{
			Evaluated = evaluated;
			Evaluator = evaluator;
			_value = value;
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

		public override void Set(T value)
		{
			_value = value;
			Evaluated = true;
		}
	}
}