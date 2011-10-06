using System;
using System.Collections.Generic;

namespace Paraiba.Utility {
	public class MonitoredWrap<T> : MutableWrap<T> {
		private T _value;
		private readonly IEqualityComparer<T> _cmp;
		public Func<T, T, T> Changing { get; set; }
		public Action<T, T> Changed { get; set; }

		public MonitoredWrap(T value)
				: this(value, EqualityComparer<T>.Default) { }

		public MonitoredWrap(T value, IEqualityComparer<T> cmp) {
			_value = value;
			_cmp = cmp;
		}

		public override T Value {
			get { return _value; }
		}

		public override void Set(T value) {
			var old = _value;
			if (!_cmp.Equals(old, value) && Changing != null)
				value = Changing(value, old);
			_value = value;
			if (!_cmp.Equals(old, value) && Changed != null)
				Changed(value, old);
		}
	}

	public static class MonitoredWrap {
		public static MonitoredWrap<T> Create<T>(T value) {
			return new MonitoredWrap<T>(value);
		}
	}
}