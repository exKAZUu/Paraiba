using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Geometry;

namespace Paraiba.Drawing
{
    public static class TwoDimensionExtensions
    {
        public static void Rotate90(this I2Dimension<int> src)
        {
            var x = src.X;
            src.X = src.Y;
            src.Y = -x;
        }

        public static void Rotate90(this I2Dimension<int> src, I2Dimension<int> pivot)
        {
            src.X -= pivot.X; src.Y -= pivot.Y;
            src.Rotate90();
            src.X += pivot.X; src.Y += pivot.Y;
        }

        public static void Rotate180(this I2Dimension<int> src)
        {
            src.X = -src.X;
            src.Y = -src.Y;
        }

        public static void Rotate180(this I2Dimension<int> src, I2Dimension<int> pivot)
        {
            src.X -= pivot.X; src.Y -= pivot.Y;
            src.Rotate180();
            src.X += pivot.X; src.Y += pivot.Y;
        }

        public static void Rotate270(this I2Dimension<int> src)
        {
            var x = src.X;
            src.X = -src.Y;
            src.Y = x;
        }

        public static void Rotate270(this I2Dimension<int> src, I2Dimension<int> pivot)
        {
            src.X -= pivot.X; src.Y -= pivot.Y;
            src.Rotate270();
            src.X += pivot.X; src.Y += pivot.Y;
        }
    }
}