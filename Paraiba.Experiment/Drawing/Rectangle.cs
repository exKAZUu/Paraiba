using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Paraiba.Drawing
{
    public static class RectangleExtensions
    {
        public static Rectangle Circumscribed(this RectangleF rect)
        {
            var left = (int)rect.Left;
            var top = (int)rect.Top;
            return new Rectangle(left, top,
                                 (int)Math.Ceiling(rect.Right) - left, (int)Math.Ceiling(rect.Bottom) - top);
        }

        public static Rectangle Round(this RectangleF rect)
        {
            return Rectangle.Round(rect);
        }
    }
}