using System.Drawing;

namespace Paraiba.Geometry
{
	public struct Vector2
	{
		private readonly int _x, _y;

		public Vector2(int x, int y)
		{
			_x = x;
			_y = y;
		}

		public Vector2(Point2 p)
		{
			_x = p.X;
			_y = p.Y;
		}

		public Vector2(Point2 to, Point2 from)
		{
			_x = to.X - from.X;
			_y = to.Y - from.Y;
		}

		public int X
		{
			get { return _x; }
		}

		public int Y
		{
			get { return _y; }
		}

		public double Angle
		{
			get { return System.Math.Atan2(Y, X); }
		}

		public double Length
		{
			get { return System.Math.Sqrt(X * X + Y * Y); }
		}

		public int LengthSq
		{
			get { return (X * X + Y * Y); }
		}

		public static Vector2 operator +(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static Vector2 operator -(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static Point operator +(Point p, Vector2 v)
		{
			return new Point(p.X + v.X, p.Y + v.Y);
		}

		public static Point2 operator +(Point2 p, Vector2 v)
		{
			return new Point2(p.X + v.X, p.Y + v.Y);
		}

		public static Vector2 operator -(Vector2 v)
		{
			return new Vector2(-v.X, -v.Y);
		}

		public static Point operator -(Point p, Vector2 v)
		{
			return new Point(p.X - v.X, p.Y - v.Y);
		}

		public static Point2 operator -(Point2 p, Vector2 v)
		{
			return new Point2(p.X - v.X, p.Y - v.Y);
		}

		public static Vector2 operator *(Vector2 v, int factor)
		{
			return new Vector2(v.X * factor, v.Y * factor);
		}

		public static Vector2F operator *(Vector2 v, float factor)
		{
			return new Vector2F(v.X * factor, v.Y * factor);
		}

		public static Vector2 operator *(int factor, Vector2 v)
		{
			return new Vector2(v.X * factor, v.Y * factor);
		}

		public static Vector2F operator *(float factor, Vector2 v)
		{
			return new Vector2F(v.X * factor, v.Y * factor);
		}

		public static Vector2 operator /(Vector2 v, int modulus)
		{
			return new Vector2(v.X / modulus, v.Y / modulus);
		}

		public static Vector2F operator /(Vector2 v, float modulus)
		{
			return new Vector2F((v.X / modulus), (v.Y / modulus));
		}

		public static bool operator ==(Vector2 v1, Vector2 v2)
		{
			return v1.Equals(v2);
		}

		public static bool operator !=(Vector2 v1, Vector2 v2)
		{
			return !v1.Equals(v2);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Vector2)) return false;
			return Equals((Vector2)obj);
		}

		public bool Equals(Vector2 v)
		{
			return v.X == X && v.Y == Y;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public static explicit operator Vector2(Point2 p)
		{
			return new Vector2(p.X, p.Y);
		}

		public static explicit operator Vector2(Point p)
		{
			return new Vector2(p.X, p.Y);
		}

		public static explicit operator Point2(Vector2 v)
		{
			return new Point2(v.X, v.Y);
		}

		public static explicit operator Point(Vector2 v)
		{
			return new Point(v.X, v.Y);
		}

		public override string ToString()
		{
			return "{X=" + X + ",Y=" + Y + "}";
		}

		/// <summary>
		/// 外積のZ成分（X, Y成分は0）を求める。
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public int CrossProduct(Vector2 v)
		{
			return X * v.Y - Y * v.X;
		}

		/// <summary>
		/// 内積を求める。
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public int InnerProduct(Vector2 v)
		{
			return X * v.X + Y * v.Y;
		}

		public Vector2 Add(Vector2 dv)
		{
			return new Vector2(X + dv.X, Y + dv.Y);
		}

		public Vector2 Rotate90()
		{
			return new Vector2(Y, -X);
		}

		public Vector2 Rotate90(Vector2 pivot)
		{
			return Add(-pivot)
				.Rotate90()
				.Add(pivot);
		}

		public Vector2 Rotate180()
		{
			return new Vector2(-X, -Y);
		}

		public Vector2 Rotate180(Vector2 pivot) {
			return Add(-pivot)
				.Rotate180()
				.Add(pivot);
		}

		public Vector2 Rotate270()
		{
			return new Vector2(-Y, X);
		}

		public Vector2 Rotate270(Vector2 pivot)
		{
			return Add(-pivot)
				.Rotate270()
				.Add(pivot);
		}
	}
}