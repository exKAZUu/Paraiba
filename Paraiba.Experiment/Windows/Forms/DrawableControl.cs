#region License

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

using System.Windows.Forms;

namespace Paraiba.Windows.Forms {
    public class DrawableControl : Control {
        public DrawableControl() {
            // ダブルバッファリングの有効
            //DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // 独自描画
            SetStyle(ControlStyles.UserPaint, true);

            // 背景描画の省略
            SetStyle(ControlStyles.Opaque, true);

            // 透明な背景色の許可
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}