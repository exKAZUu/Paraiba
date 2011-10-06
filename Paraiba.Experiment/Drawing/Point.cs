using System.Drawing;

namespace Paraiba.Drawing
{
    public static class PointExtensions
    {
        public static Point GetTopLeftToCenter(this Point p, Size size)
        {
            return new Point(p.X + (size.Width >> 1), p.Y + (size.Height >> 1));
        }

        public static Point GetLeftToCenter(this Point p, Size size)
        {
            return new Point(p.X + (size.Width >> 1), p.Y);
        }

        public static Point GetTopToCenter(this Point p, Size size)
        {
            return new Point(p.X, p.Y + (size.Height >> 1));
        }

        public static Point GetCenterToTopLeft(this Point p, Size size)
        {
            return new Point(p.X - (size.Width >> 1), p.Y - (size.Height >> 1));
        }

        public static Point GetTopCenterToTopLeft(this Point p, Size size)
        {
            return new Point(p.X - (size.Width >> 1), p.Y);
        }

        public static Point GetCenterLeftToTopLeft(this Point p, Size size)
        {
            return new Point(p.X, p.Y - (size.Height >> 1));
        }

        public static void MoveTopLeftToCenter(this Point p, Size size)
        {
            p.X += (size.Width >> 1);
            p.Y += (size.Height >> 1);
        }

        public static void MoveLeftToCenter(this Point p, Size size)
        {
            p.X += (size.Width >> 1);
        }

        public static void MoveTopToCenter(this Point p, Size size)
        {
            p.Y += (size.Height >> 1);
        }

        public static void MoveCenterToTopLeft(this Point p, Size size)
        {
            p.X -= size.Width >> 1;
            p.Y -= size.Height >> 1;
        }

        public static void MoveTopCenterToTopLeft(this Point p, Size size)
        {
            p.X -= size.Width >> 1;
        }

        public static void MoveCenterLeftToTopLeft(this Point p, Size size)
        {
            p.Y -= size.Height >> 1;
        }
    }
}