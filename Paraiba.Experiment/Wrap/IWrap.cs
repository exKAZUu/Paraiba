namespace Paraiba.Wrap {
	public interface IWrap<out T>
	{
		T Value { get; }
	}
}