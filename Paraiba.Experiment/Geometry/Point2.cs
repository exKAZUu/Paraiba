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

using System.Drawing;

namespace Paraiba.Geometry {
	public struct Point2 {
		private readonly int _x, _y;

		public Point2(int x, int y) {
			_x = x;
			_y = y;
		}

		public int X {
			get { return _x; }
		}

		public int Y {
			get { return _y; }
		}

		public static Point2 operator +(Point2 p, Size s) {
			return new Point2(p.X + s.Width, p.Y + s.Height);
		}

		public static Point2 operator -(Point2 p, Size s) {
			return new Point2(p.X - s.Width, p.Y - s.Height);
		}

		public static Vector2 operator -(Point2 p1, Point2 p2) {
			return new Vector2(p1.X - p2.X, p1.Y - p2.Y);
		}

		public static bool operator ==(Point2 p, Point2 q) {
			return p.Equals(q);
		}

		public static bool operator !=(Point2 p, Point2 q) {
			return !p.Equals(q);
		}

		public override bool Equals(object obj) {
			if (!(obj is Point2)) {
				return false;
			}
			return Equals((Point2)obj);
		}

		public bool Equals(Point2 p) {
			return p.X == X && p.Y == Y;
		}

		public bool Equals(Point p) {
			return false;
		}

		public override int GetHashCode() {
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public static implicit operator Point(Point2 p) {
			return new Point(p.X, p.Y);
		}

		public static implicit operator Point2(Point p) {
			return new Point2(p.X, p.Y);
		}

		public override string ToString() {
			return "{X=" + X + ",Y=" + Y + "}";
		}

		public Point2 GetTopLeftToCenter(Size size) {
			return new Point2(X + (size.Width >> 1), Y + (size.Height >> 1));
		}

		public Point2 GetLeftToCenter(Size size) {
			return new Point2(X + (size.Width >> 1), Y);
		}

		public Point2 GetTopToCenter(Size size) {
			return new Point2(X, Y + (size.Height >> 1));
		}

		public Point2 GetTopToBottom(Size size) {
			return new Point2(X, Y + size.Height);
		}

		public Point2 GetBottomToTop(Size size) {
			return new Point2(X, Y - size.Height);
		}

		public Point2 GetCenterLeftToTopLeft(Size size) {
			return new Point2(X, Y - (size.Height >> 1));
		}

		public Point2 GetCenterToTopLeft(Size size) {
			return new Point2(X - (size.Width >> 1), Y - (size.Height >> 1));
		}

		public Point2 GetTopCenterToTopLeft(Size size) {
			return new Point2(X - (size.Width >> 1), Y);
		}

		public Point2 Offset(int dx, int dy) {
			return new Point2(X + dx, Y + dy);
		}

		public Point2 Offset(Point2 dp) {
			return new Point2(X + dp.X, Y + dp.Y);
		}

		public Point2 Add(Vector2 dv) {
			return new Point2(X + dv.X, Y + dv.Y);
		}

		public Point2 Rotate90() {
			return new Point2(Y, -X);
		}

		public Point2 Rotate90(Vector2 pivot) {
			return Add(-pivot)
					.Rotate90()
					.Add(pivot);
		}

		public Point2 Rotate180() {
			return new Point2(-X, -Y);
		}

		public Point2 Rotate180(Vector2 pivot) {
			return Add(-pivot)
					.Rotate180()
					.Add(pivot);
		}

		public Point2 Rotate270() {
			return new Point2(-Y, X);
		}

		public Point2 Rotate270(Vector2 pivot) {
			return Add(-pivot)
					.Rotate270()
					.Add(pivot);
		}
	}
}