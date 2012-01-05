namespace Paraiba.Wrap {
	public interface IMutableWrap<T> : IWrap<T>
	{
		void Set(T value);
	}
}