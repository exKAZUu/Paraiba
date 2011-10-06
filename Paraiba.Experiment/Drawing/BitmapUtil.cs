using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Paraiba.Drawing
{
	public class BitmapUtil
	{
		private static Bitmap PrivateLoadOnMemory(string fpath)
		{
			using (var fs = new FileStream(fpath, FileMode.Open, FileAccess.Read))
			{
				var buffer = new byte[fs.Length];
				fs.Read(buffer, 0, buffer.Length);
				fs.Close();
				var bmp = new Bitmap(new MemoryStream(buffer));
				return bmp;
			}
		}

		public static Bitmap LoadOnMemory(string fpath)
		{
			var bmp = PrivateLoadOnMemory(fpath);
			bmp.SetResolution();
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、標準解像度の Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath)
		{
			var bmp = new Bitmap(fpath);
			bmp.SetResolution();
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定した透明色を使用した標準解像度の Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="transparentColor">使用する透明色</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, Color transparentColor)
		{
			var bmp = new Bitmap(fpath);
			bmp.MakeTransparent(transparentColor);
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定した透明色を使用した標準解像度の Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="transparentColor">使用する透明色</param>
		/// <param name="allowTransparentIfHasAlpha">アルファ値を既に保持していた場合、透明色を使用するかどうか</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, Color transparentColor, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
				bmp.MakeTransparent(transparentColor);
			else
				bmp.SetResolution();
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定したピクセル位置の色を透明色として使用した標準解像度の Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="transparentColorPoint">透明色として使用する色のピクセル位置</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, Point transparentColorPoint)
		{
			var bmp = new Bitmap(fpath);
			if (!bmp.TryMakeTransparent(transparentColorPoint))
				bmp.SetResolution();
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定したピクセル位置の色を透明色として使用した標準解像度の Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="transparentColorPoint">透明色として使用する色のピクセル位置</param>
		/// <param name="allowTransparentIfHasAlpha">アルファ値を既に保持していた場合、透明色を使用するかどうか</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, Point transparentColorPoint, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
				bmp.TryMakeTransparent(transparentColorPoint);
			bmp.SetResolution();
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="ignoreResolution">解像度を無視して標準値を設定するかどうか</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, bool ignoreResolution)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定した透明色を使用した Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="ignoreResolution">解像度を無視して標準値を設定するかどうか</param>
		/// <param name="transparentColor">使用する透明色</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, bool ignoreResolution, Color transparentColor)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();
			var hres = bmp.HorizontalResolution;
			var vres = bmp.VerticalResolution;
			bmp.MakeTransparent(transparentColor);
			bmp.SetResolution(hres, vres);
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定した透明色を使用した Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="ignoreResolution">解像度を無視して標準値を設定するかどうか</param>
		/// <param name="transparentColor">使用する透明色</param>
		/// <param name="allowTransparentIfHasAlpha">アルファ値を既に保持していた場合、透明色を使用するかどうか</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, bool ignoreResolution, Color transparentColor, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				var hres = bmp.HorizontalResolution;
				var vres = bmp.VerticalResolution;
				bmp.MakeTransparent(transparentColor);
				bmp.SetResolution(hres, vres);
			}
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定したピクセル位置の色を透明色として使用した Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="ignoreResolution">解像度を無視して標準値を設定するかどうか</param>
		/// <param name="transparentColorPoint">透明色として使用する色のピクセル位置</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, bool ignoreResolution, Point transparentColorPoint)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();
			var hres = bmp.HorizontalResolution;
			var vres = bmp.VerticalResolution;
			if (bmp.TryMakeTransparent(transparentColorPoint))
				bmp.SetResolution(hres, vres);
			return bmp;
		}

		/// <summary>
		/// 指定したファイルを読み込んで、指定したピクセル位置の色を透明色として使用した Bitmap を作成します。
		/// </summary>
		/// <param name="fpath">読み込むファイルのパス</param>
		/// <param name="ignoreResolution">解像度を無視して標準値を設定するかどうか</param>
		/// <param name="transparentColorPoint">透明色として使用する色のピクセル位置</param>
		/// <param name="allowTransparentIfHasAlpha">アルファ値を既に保持していた場合、透明色を使用するかどうか</param>
		/// <returns>作成した Bitmap</returns>
		public static Bitmap Load(string fpath, bool ignoreResolution, Point transparentColorPoint, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				var hres = bmp.HorizontalResolution;
				var vres = bmp.VerticalResolution;
				if (bmp.TryMakeTransparent(transparentColorPoint))
					bmp.SetResolution(hres, vres);
			}
			return bmp;
		}

		public static Bitmap LoadFullColor(string fpath)
		{
			using (var bmp = new Bitmap(fpath))
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColor(string fpath, Color transparentColor)
		{
			var bmp = new Bitmap(fpath);
			bmp.MakeTransparent(transparentColor);
			return bmp;
		}

		public static Bitmap LoadFullColor(string fpath, Color transparentColor, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				bmp.MakeTransparent(transparentColor);
				return bmp;
			}

			using (bmp)
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColor(string fpath, Point transparentColorPoint)
		{
			var bmp = new Bitmap(fpath);
			if (bmp.TryMakeTransparent(transparentColorPoint))
				return bmp;

			using (bmp)
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColor(string fpath, Point transparentColorPoint, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				if (bmp.TryMakeTransparent(transparentColorPoint))
					return bmp;
			}

			using (bmp)
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColor(string fpath, bool ignoreResolution)
		{
			using (var bmp = new Bitmap(fpath))
			{
				if (ignoreResolution) bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColor(string fpath, bool ignoreResolution, Color transparentColor)
		{
			return Load(fpath, ignoreResolution, transparentColor);
		}

		public static Bitmap LoadFullColor(string fpath, bool ignoreResolution, Color transparentColor, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				var hres = bmp.HorizontalResolution;
				var vres = bmp.VerticalResolution;
				bmp.MakeTransparent(transparentColor);
				bmp.SetResolution(hres, vres);
				return bmp;
			}

			using (bmp)
			{
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColor(string fpath, bool ignoreResolution, Point transparentColorPoint)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();
			var hres = bmp.HorizontalResolution;
			var vres = bmp.VerticalResolution;
			if (bmp.TryMakeTransparent(transparentColorPoint))
			{
				bmp.SetResolution(hres, vres);
				return bmp;
			}

			using (bmp)
			{
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColor(string fpath, bool ignoreResolution, Point transparentColorPoint, bool allowTransparentIfHasAlpha)
		{
			var bmp = new Bitmap(fpath);
			if (ignoreResolution) bmp.SetResolution();

			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				var hres = bmp.HorizontalResolution;
				var vres = bmp.VerticalResolution;
				if (bmp.TryMakeTransparent(transparentColorPoint))
				{
					bmp.SetResolution(hres, vres);
					return bmp;
				}
			}

			using (bmp)
			{
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColorOnMemory(string fpath)
		{
			using (var bmp = PrivateLoadOnMemory(fpath))
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColorOnMemory(string fpath, Color transparentColor)
		{
			return LoadFullColor(fpath, transparentColor);
		}

		public static Bitmap LoadFullColorOnMemory(string fpath, Color transparentColor, bool allowTransparentIfHasAlpha)
		{
			var bmp = PrivateLoadOnMemory(fpath);
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				bmp.MakeTransparent(transparentColor);
				return bmp;
			}

			using (bmp)
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColorOnMemory(string fpath, Point transparentColorPoint)
		{
			var bmp = PrivateLoadOnMemory(fpath);
			if (bmp.TryMakeTransparent(transparentColorPoint))
				return bmp;

			using (bmp)
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}

		public static Bitmap LoadFullColorOnMemory(string fpath, Point transparentColorPoint, bool allowTransparentIfHasAlpha)
		{
			var bmp = PrivateLoadOnMemory(fpath);
			if (allowTransparentIfHasAlpha || (bmp.Flags & (int)ImageFlags.HasAlpha) == 0)
			{
				if (bmp.TryMakeTransparent(transparentColorPoint))
					return bmp;
			}

			using (bmp)
			{
				bmp.SetResolution();
				return bmp.Clone(PixelFormat.Format32bppArgb);
			}
		}
	}
}