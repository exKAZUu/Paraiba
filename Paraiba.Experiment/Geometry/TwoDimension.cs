namespace Paraiba.Geometry
{
	public interface I2Dimension<T>
	{
		T X { get; set; }
		T Y { get; set; }
	}

	public interface I3Dimension<T>
	{
		T X { get; set; }
		T Y { get; set; }
		T Z { get; set; }
	}
}