#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Drawing;
using System.Drawing.Imaging;
using Paraiba.Drawing.Surfaces;
using Paraiba.Wrap;

namespace Paraiba.Drawing {
	public static class BitmapExtensionMethod {
		public static Bitmap ChangePixelFormat(
				this Bitmap bmp, PixelFormat format) {
			return bmp.Clone(new Rectangle(Point.Empty, bmp.Size), format);
		}

		public static Bitmap ChangePixelFormat(
				this Bitmap bmp, PixelFormat format,
				PixelFormat formatIfNotHasAlpha) {
			if ((bmp.Flags & (int)ImageFlags.HasAlpha) == 0) {
				return bmp.Clone(new Rectangle(Point.Empty, bmp.Size), format);
			}
			return bmp.Clone(
					new Rectangle(Point.Empty, bmp.Size), formatIfNotHasAlpha);
		}

		public static Bitmap Clone(this Bitmap bmp, PixelFormat format) {
			return bmp.Clone(new Rectangle(Point.Empty, bmp.Size), format);
		}

		public static void MakeTransparent(
				this Bitmap bmp, Point transparentColorPoint) {
			var x = transparentColorPoint.X;
			var y = transparentColorPoint.Y;

			bmp.MakeTransparent(bmp.GetPixel(x, y));
		}

		public static bool TryMakeTransparent(
				this Bitmap bmp, Point transparentColorPoint) {
			var x = transparentColorPoint.X;
			var y = transparentColorPoint.Y;

			if (0 <= x && x < bmp.Width && 0 <= y && y < bmp.Height) {
				bmp.MakeTransparent(bmp.GetPixel(x, y));
				return true;
			}
			return false;
		}

		public static void SetResolution(this Bitmap bmp) {
			bmp.SetResolution(96, 96);
		}

		public static T[] SplitTo<T>(
				this Image image, int chipWidth, int chipHeight, int maxCount,
				Func<Rectangle, T> createTFunc) {
			int imageWidth = image.Width;
			int nWidth = imageWidth / chipWidth;
			int nHeight = image.Height / chipHeight;
			var chips = new T[Math.Min(maxCount, nWidth * nHeight)];
			imageWidth = chipWidth * nWidth;
			for (int i = 0, x = 0, y = 0; i < chips.Length; i++) {
				chips[i] =
						createTFunc(new Rectangle(x, y, chipWidth, chipHeight));
				if (x < imageWidth) {
					x += chipWidth;
				} else {
					x = 0;
					y += chipHeight;
				}
			}
			return chips;
		}

		public static T[] SplitTo<T>(
				this Image image, int chipWidth, int chipHeight,
				Func<Rectangle, T> createTFunc) {
			int nWidth = image.Width / chipWidth;
			int nHeight = image.Height / chipHeight;
			var chips = new T[nWidth * nHeight];
			for (int y = 0; y < nHeight; y++) {
				for (int x = 0; x < nWidth; x++) {
					chips[y * nWidth + x] =
							createTFunc(
									new Rectangle(
											x * chipWidth, y * chipHeight,
											chipWidth, chipHeight));
				}
			}
			return chips;
		}

		public static Bitmap[] SplitToBitmaps(
				this Bitmap bmp, int chipWidth, int chipHeight) {
			return bmp.SplitTo(
					chipWidth, chipHeight,
					rect => bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Bitmap[] SplitToBitmaps(
				this Bitmap bmp, int chipWidth, int chipHeight,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipWidth, chipHeight, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip;
					});
		}

		public static Bitmap[] SplitToBitmaps(
				this Bitmap bmp, int chipWidth, int chipHeight, int maxCount) {
			return bmp.SplitTo(
					chipWidth, chipHeight, maxCount,
					rect => bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Bitmap[] SplitToBitmaps(
				this Bitmap bmp, int chipWidth, int chipHeight, int maxCount,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipWidth, chipHeight, maxCount, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip;
					});
		}

		public static Bitmap[] SplitToBitmaps(this Bitmap bmp, Size chipSize) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height,
					rect => bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Bitmap[] SplitToBitmaps(
				this Bitmap bmp, Size chipSize, Point transparentColorPoint) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip;
					});
		}

		public static Bitmap[] SplitToBitmaps(
				this Bitmap bmp, Size chipSize, int maxCount) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, maxCount,
					rect => bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Bitmap[] SplitToBitmaps(
				this Bitmap bmp, Size chipSize, int maxCount,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, maxCount, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip;
					});
		}

		public static Image[] SplitToImages(
				this Bitmap bmp, int chipWidth, int chipHeight) {
			return bmp.SplitTo(
					chipWidth, chipHeight,
					rect => (Image)bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Image[] SplitToImages(
				this Bitmap bmp, int chipWidth, int chipHeight,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipWidth, chipHeight, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return (Image)chip;
					});
		}

		public static Image[] SplitToImages(
				this Bitmap bmp, int chipWidth, int chipHeight, int maxCount) {
			return bmp.SplitTo(
					chipWidth, chipHeight, maxCount,
					rect => (Image)bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Image[] SplitToImages(
				this Bitmap bmp, int chipWidth, int chipHeight, int maxCount,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipWidth, chipHeight, maxCount, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return (Image)chip;
					});
		}

		public static Image[] SplitToImages(this Bitmap bmp, Size chipSize) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height,
					rect => (Image)bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Image[] SplitToImages(
				this Bitmap bmp, Size chipSize, Point transparentColorPoint) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return (Image)chip;
					});
		}

		public static Image[] SplitToImages(
				this Bitmap bmp, Size chipSize, int maxCount) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, maxCount,
					rect => (Image)bmp.Clone(rect, bmp.PixelFormat));
		}

		public static Image[] SplitToImages(
				this Bitmap bmp, Size chipSize, int maxCount,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, maxCount, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return (Image)chip;
					});
		}

		public static Surface[] SplitToSurfaces(
				this Bitmap bmp, int chipWidth, int chipHeight) {
			return bmp.SplitTo(
					chipWidth, chipHeight,
					rect => bmp.Clone(rect, bmp.PixelFormat).ToSurface());
		}

		public static Surface[] SplitToSurfaces(
				this Bitmap bmp, int chipWidth, int chipHeight,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipWidth, chipHeight, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip.ToSurface();
					});
		}

		public static Surface[] SplitToSurfaces(
				this Bitmap bmp, int chipWidth, int chipHeight, int maxCount) {
			return bmp.SplitTo(
					chipWidth, chipHeight, maxCount,
					rect => bmp.Clone(rect, bmp.PixelFormat).ToSurface());
		}

		public static Surface[] SplitToSurfaces(
				this Bitmap bmp, int chipWidth, int chipHeight, int maxCount,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipWidth, chipHeight, maxCount, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip.ToSurface();
					});
		}

		public static Surface[] SplitToSurfaces(this Bitmap bmp, Size chipSize) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height,
					rect => bmp.Clone(rect, bmp.PixelFormat).ToSurface());
		}

		public static Surface[] SplitToSurfaces(
				this Bitmap bmp, Size chipSize, Point transparentColorPoint) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip.ToSurface();
					});
		}

		public static Surface[] SplitToSurfaces(
				this Bitmap bmp, Size chipSize, int maxCount) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, maxCount,
					rect => bmp.Clone(rect, bmp.PixelFormat).ToSurface());
		}

		public static Surface[] SplitToSurfaces(
				this Bitmap bmp, Size chipSize, int maxCount,
				Point transparentColorPoint) {
			return bmp.SplitTo(
					chipSize.Width, chipSize.Height, maxCount, rect => {
						var chip = bmp.Clone(rect, bmp.PixelFormat);
						chip.TryMakeTransparent(transparentColorPoint);
						return chip.ToSurface();
					});
		}

		public static Surface ToSurface(this Bitmap bmp) {
			return new BitmapSurface(bmp);
		}

		public static Surface ToSurface(this Wrap<Bitmap> bmp) {
			return new BitmapSurface(bmp);
		}
	}
}