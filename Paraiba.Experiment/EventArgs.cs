using System;

namespace Paraiba
{
	public class PropertyChangeEventArgs<T> : EventArgs
	{
		public PropertyChangeEventArgs(T newValue, T oldValue)
		{
			NewValue = newValue;
			OldValue = oldValue;
		}

		public T NewValue { get; private set; }

		public T OldValue { get; private set; }
	}
}