namespace Paraiba.Utility {
	public class MutableValueWrap<T> : MutableWrap<T>
	{
		private T _value;

		public MutableValueWrap(T value)
		{
			_value = value;
		}

		public override T Value
		{
			get { return _value; }
		}

		public override void Set(T value)
		{
			_value = value;
		}

		public static implicit operator MutableValueWrap<T>(T value)
		{
			return new MutableValueWrap<T>(value);
		}
	}
}