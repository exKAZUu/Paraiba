﻿#region License

// Copyright (C) 2011-2016 Kazunori Sakamoto
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
using System.Drawing.Drawing2D;

namespace Paraiba.Drawing.Surfaces {
    public class StringPathSurface : Surface {
        private readonly Brush _brush;
        private readonly GraphicsPath _path;
        private int _lastX, _lastY;
        private Matrix _matrix;

        public StringPathSurface(String str, Font font, Brush brush)
            : this(str, font, brush, null) {}

        public StringPathSurface(
            String str, Font font, Brush brush, StringFormat stringFormat) {
            Contract.Requires(str != null);
            Contract.Requires(font != null);
            Contract.Requires(brush != null);

            _brush = brush;

            // グラフィックスパスの生成
            _path = new GraphicsPath();
            _path.AddString(
                str, font.FontFamily, (int)font.Style, font.Size,
                new Point(0, 0), stringFormat);
            // サイズを取得する
            var rect = _path.GetBounds();
            Size = rect.Size.ToSize();
            // 描画時にマージンがなくなるように平行移動
            var matrix = new Matrix(1, 0, 0, 1, -rect.Left, -rect.Top);
            _path.Transform(matrix);
            // 描画位置を(0, 0)で記憶
            matrix.Reset();
            _matrix = matrix;
        }

        public override Size Size { get; }

        public override void Draw(Graphics g, int x, int y) {
            var matrix = _matrix;
            // 前回の描画位置との差分を計算
            //matrix = new Matrix(1, 0, 0, 1, x - _lastX, y - _lastY);
            matrix.Translate(
                (x - _lastX) - matrix.OffsetX, (y - _lastY) - matrix.OffsetY);
            _lastX = x;
            _lastY = y;
            // 計算した差分だけ平行移動
            _path.Transform(matrix);
            _matrix = matrix;

            var oldMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillPath(_brush, _path);

            g.SmoothingMode = oldMode;
        }
    }
}