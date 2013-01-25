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

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces {
	/// <summary>
	///   １つのスプライトを指定した差分位置すべてに同じ内容で表示するスプライトを表します。
	/// </summary>
	public class CopySurface : Surface {
		private readonly IEnumerable<Vector2> _offsetVectors;
		private readonly Surface _surface;

		public CopySurface(
				Surface originalSurface, IEnumerable<Vector2> offsetVectors) {
			Contract.Requires(originalSurface != null);
			Contract.Requires(offsetVectors != null);

			_surface = originalSurface;
			_offsetVectors = offsetVectors;
		}

		public override Size Size {
			get { return _surface.Size; }
		}

		public override void Draw(Graphics g, int x, int y) {
			_surface.Draw(g, x, y);
			foreach (Vector2 v in _offsetVectors) {
				_surface.Draw(g, x + v.X, y + v.Y);
			}
		}

		public override Image GetImage() {
			return _surface.GetImage();
		}
	}
}