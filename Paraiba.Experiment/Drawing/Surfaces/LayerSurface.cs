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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using Paraiba.Windows.Forms;

namespace Paraiba.Drawing.Surfaces {
	public class LayerSurface : Surface {
		private readonly IEnumerable<Surface> _surfaces;
		private Size _size;

		public LayerSurface(params Surface[] surfaces)
				: this((IList<Surface>)surfaces) {}

		public LayerSurface(IEnumerable<Surface> surfaces)
				: this(surfaces, surfaces.First().Size) {}

		public LayerSurface(IList<Surface> surfaces)
				: this(surfaces, surfaces[0].Size) {}

		public LayerSurface(
				IEnumerable<Surface> surfaces, int width, int height)
				: this(surfaces, new Size(width, height)) {}

		public LayerSurface(IEnumerable<Surface> surfaces, Size size) {
			Contract.Requires(surfaces != null);

			_surfaces = surfaces;
			_size = size;
		}

		public HorizontalLocationStyle HorizontalLocationStyle { get; set; }

		public VerticalLocationStyle VerticalLocationStyle { get; set; }

		/// <summary>
		///   オブジェクトの縦横のサイズを取得します。
		/// </summary>
		public override Size Size {
			get { return _size; }
		}

		/// <summary>
		///   オブジェクトの左上原点が指定した位置に来るように描画します。
		/// </summary>
		/// <param name="g"> 描画で利用する Graphics </param>
		/// <param name="x"> 描画位置のX座標 </param>
		/// <param name="y"> 描画位置のY座標 </param>
		public override void Draw(Graphics g, int x, int y) {
			foreach (var s in _surfaces) {
				s.Draw(g, GetDrawX(s, x), GetDrawY(s, y));
			}
		}

		private int GetDrawY(Surface surface, int y) {
			switch (VerticalLocationStyle) {
			case VerticalLocationStyle.Top:
				return y;
			case VerticalLocationStyle.Center:
				return (_size.Height - surface.Height) / 2;
			case VerticalLocationStyle.Bottom:
				return _size.Height - surface.Height;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private int GetDrawX(Surface surface, int x) {
			switch (HorizontalLocationStyle) {
			case HorizontalLocationStyle.Left:
				return x;
			case HorizontalLocationStyle.Center:
				return (_size.Width - surface.Width) / 2;
			case HorizontalLocationStyle.Right:
				return _size.Width - surface.Width;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}