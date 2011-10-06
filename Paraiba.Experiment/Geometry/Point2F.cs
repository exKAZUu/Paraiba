using System;
using System.Drawing;

namespace Paraiba.Geometry
{
	public struct Point2F
	{
		#region Copy & Paste from Point2

		private readonly float _x, _y;

		public Point2F(float x, float y)
		{
			_x = x;
			_y = y;
		}

		public float X
		{
			get { return _x; }
		}

		public float Y
		{
			get { return _y; }
		}

		public static Point2F operator +(Point2F p, Size s)
		{
			return new Point2F(p.X + s.Width, p.Y + s.Height);
		}

		public static Point2F operator -(Point2F p, Size s)
		{
			return new Point2F(p.X - s.Width, p.Y - s.Height);
		}

		public static Vector2F operator -(Point2F p1, Point2F p2)
		{
			return new Vector2F(p1.X - p2.X, p1.Y - p2.Y);
		}

		public static bool operator ==(Point2F p, Point2F q)
		{
			return p.Equals(q);
		}

		public static bool operator !=(Point2F p, Point2F q)
		{
			return !p.Equals(q);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Point2F)) return false;
			return Equals((Point2F)obj);
		}

		public bool Equals(Point2F p)
		{
			return p.X == X && p.Y == Y;
		}

		public bool Equals(PointF p) {
			return false;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public static implicit operator PointF(Point2F p)
		{
			return new PointF(p.X, p.Y);
		}

		public static implicit operator Point2F(PointF p)
		{
			return new Point2F(p.X, p.Y);
		}

		public override string ToString()
		{
			return "{X=" + X + ",Y=" + Y + "}";
		}

		public Point2F GetTopLeftToCenter(Size size)
		{
			return new Point2F(X + (size.Width >> 1), Y + (size.Height >> 1));
		}

		public Point2F GetLeftToCenter(Size size)
		{
			return new Point2F(X + (size.Width >> 1), Y);
		}

		public Point2F GetTopToCenter(Size size)
		{
			return new Point2F(X, Y + (size.Height >> 1));
		}

		public Point2F GetTopToBottom(Size size)
		{
			return new Point2F(X, Y + size.Height);
		}

		public Point2F GetBottomToTop(Size size)
		{
			return new Point2F(X, Y - size.Height);
		}

		public Point2F GetCenterLeftToTopLeft(Size size)
		{
			return new Point2F(X, Y - (size.Height >> 1));
		}

		public Point2F GetCenterToTopLeft(Size size)
		{
			return new Point2F(X - (size.Width >> 1), Y - (size.Height >> 1));
		}

		public Point2F GetTopCenterToTopLeft(Size size)
		{
			return new Point2F(X - (size.Width >> 1), Y);
		}

		public Point2F Offset(float dx, float dy)
		{
			return new Point2F(X + dx, Y + dy);
		}

		public Point2F Offset(Point2F dp)
		{
			return new Point2F(X + dp.X, Y + dp.Y);
		}

		public Point2F Add(Vector2F dv)
		{
			return new Point2F(X + dv.X, Y + dv.Y);
		}

		public Point2F Rotate90()
		{
			return new Point2F(Y, -X);
		}

		public Point2F Rotate90(Vector2F pivot)
		{
			return Add(-pivot)
				.Rotate90()
				.Add(pivot);
		}

		public Point2F Rotate180()
		{
			return new Point2F(-X, -Y);
		}

		public Point2F Rotate180(Vector2F pivot) {
			return Add(-pivot)
				.Rotate180()
				.Add(pivot);
		}

		public Point2F Rotate270()
		{
			return new Point2F(-Y, X);
		}

		public Point2F Rotate270(Vector2F pivot)
		{
			return Add(-pivot)
				.Rotate270()
				.Add(pivot);
		}

		#endregion

		public static implicit operator Point2F(Point2 p)
		{
			return new Point2F(p.X, p.Y);
		}

		public static implicit operator Point2F(Point p)
		{
			return new Point2F(p.X, p.Y);
		}

		public static explicit operator Point2(Point2F p)
		{
			return new Point2((int)p.X, (int)p.Y);
		}

		public static explicit operator Point(Point2F p)
		{
			return new Point((int)p.X, (int)p.Y);
		}

		public Point2 ToRoundedPoint2()
		{
			return new Point2((int)(X + 0.5), (int)(Y + 0.5));
		}

		public bool Equals(Point2 p)
		{
			return false;
		}

		public bool Equals(Point p)
		{
			return false;
		}
	}
}