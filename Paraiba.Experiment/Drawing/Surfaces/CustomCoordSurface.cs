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
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces {
	public class CustomCoordSurface : Surface {
		private readonly Func<Point2, Size, Point2> _coordinateTransformer;
		private readonly Surface _surface;

		public CustomCoordSurface(
				Surface surface,
				Func<Point2, Size, Point2> coordinateTransformer) {
			Contract.Requires(surface != null);
			Contract.Requires(coordinateTransformer != null);

			_surface = surface;
			_coordinateTransformer = coordinateTransformer;
		}

		public override Size Size {
			get { return _surface.Size; }
		}

		public override void Draw(Graphics g, int x, int y) {
			// 最後に生成した CustomCoordSurface が最初に適用される
			var p = _coordinateTransformer(new Point2(x, y), _surface.Size);
			_surface.Draw(g, p.X, p.Y);
		}

		public override Image GetImage() {
			return _surface.GetImage();
		}
	}
}