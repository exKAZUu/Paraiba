namespace Paraiba.Wrap {
	public class ValueWrap<T> : Wrap<T>
	{
		private readonly T _value;

		public ValueWrap(T value)
		{
			_value = value;
		}

		public override T Value
		{
			get { return _value; }
		}

		public static implicit operator ValueWrap<T>(T value)
		{
			return new ValueWrap<T>(value);
		}
	}
}