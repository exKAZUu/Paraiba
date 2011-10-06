using System;

namespace Paraiba.Utility {
	public class MutableLazyWrap<T> : MutableWrap<T>
	{
		private Func<T> _evaluator;
		private T _value;

		public MutableLazyWrap(Func<T> evaluator)
		{
			_evaluator = evaluator;
		}

		public bool Evaluated
		{
			get { return _evaluator == null; }
		}

		public override T Value
		{
			get
			{
				if (_evaluator != null)
				{
					_value = _evaluator();
					_evaluator = null;
				}
				return _value;
			}
		}

		public override void Set(T value)
		{
			_value = value;
			_evaluator = null;
		}
	}
}