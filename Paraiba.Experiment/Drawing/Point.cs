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

namespace Paraiba.Drawing {
	public static class PointExtensions {
		public static Point GetTopLeftToCenter(this Point p, Size size) {
			return new Point(p.X + (size.Width >> 1), p.Y + (size.Height >> 1));
		}

		public static Point GetLeftToCenter(this Point p, Size size) {
			return new Point(p.X + (size.Width >> 1), p.Y);
		}

		public static Point GetTopToCenter(this Point p, Size size) {
			return new Point(p.X, p.Y + (size.Height >> 1));
		}

		public static Point GetCenterToTopLeft(this Point p, Size size) {
			return new Point(p.X - (size.Width >> 1), p.Y - (size.Height >> 1));
		}

		public static Point GetTopCenterToTopLeft(this Point p, Size size) {
			return new Point(p.X - (size.Width >> 1), p.Y);
		}

		public static Point GetCenterLeftToTopLeft(this Point p, Size size) {
			return new Point(p.X, p.Y - (size.Height >> 1));
		}

		public static void MoveTopLeftToCenter(this Point p, Size size) {
			p.X += (size.Width >> 1);
			p.Y += (size.Height >> 1);
		}

		public static void MoveLeftToCenter(this Point p, Size size) {
			p.X += (size.Width >> 1);
		}

		public static void MoveTopToCenter(this Point p, Size size) {
			p.Y += (size.Height >> 1);
		}

		public static void MoveCenterToTopLeft(this Point p, Size size) {
			p.X -= size.Width >> 1;
			p.Y -= size.Height >> 1;
		}

		public static void MoveTopCenterToTopLeft(this Point p, Size size) {
			p.X -= size.Width >> 1;
		}

		public static void MoveCenterLeftToTopLeft(this Point p, Size size) {
			p.Y -= size.Height >> 1;
		}
	}
}