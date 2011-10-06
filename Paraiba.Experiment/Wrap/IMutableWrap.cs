namespace Paraiba.Utility {
	public interface IMutableWrap<T> : IWrap<T>
	{
		void Set(T value);
	}
}