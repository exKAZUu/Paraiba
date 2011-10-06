using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Utility;

namespace Paraiba.Drawing
{
	public static class ImageExtensionMethod
	{
		public static int EstimateMemorySize(this Image image)
		{
			return image.Width * image.Height * Image.GetPixelFormatSize(image.PixelFormat) / 8;
		}

		public static Surface ToSurface(this Image bmp)
		{
			return new ImageSurface(bmp);
		}

		public static Surface ToSurface(this Wrap<Image> bmp)
		{
			return new ImageSurface(bmp);
		}
	}
}