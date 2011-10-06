namespace Paraiba.Utility {
	public interface IWrap<out T>
	{
		T Value { get; }
	}
}