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

namespace Paraiba.Drawing.Surfaces {
	/// <summary>
	///   表示可能な文字列のオブジェクトを表します。
	/// </summary>
	public class StringSurface : Surface {
		private readonly Brush _brush;
		private readonly Font _font;
		private readonly Size _size;
		private readonly String _str;
		private readonly StringFormat _stringFormat;

		public StringSurface(Graphics g, String str, Font font, Brush brush)
				: this(g, str, font, brush, null) {}

		public StringSurface(
				Graphics g, String str, Font font, Brush brush,
				StringFormat stringFormat) {
			Contract.Requires(str != null);
			Contract.Requires(font != null);
			Contract.Requires(brush != null);

			_str = str;
			_font = font;
			_brush = brush;
			_stringFormat = stringFormat;
			_size = g.MeasureString(_str, _font).ToSize();
		}

		public override Size Size {
			get { return _size; }
		}

		public override void Draw(Graphics g, int x, int y) {
			g.DrawString(_str, _font, _brush, x, y, _stringFormat);
		}
	}
}