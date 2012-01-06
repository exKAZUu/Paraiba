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
using System.Drawing;
using System.Windows.Forms;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Windows.Forms {
    public class BasicSurfaceControl<TSurface> : ParasiticControl
            where TSurface : Surface {
        protected bool autoPreferSize_;

        protected Func<BasicSurfaceControl<TSurface>, Point2>
                getControlLocationFunc_;

        protected Func<BasicSurfaceControl<TSurface>, Point2>
                getDrawLocationFunc_;

        protected TSurface surface_;

        protected BasicSurfaceControl(TSurface surface) {
            surface_ = surface;

            ArrangeSizeAndLocation();
        }

        public TSurface Surface {
            get { return surface_; }
            set { surface_ = value; }
        }

        public bool AutoPreferSize {
            get { return autoPreferSize_; }
            set { autoPreferSize_ = value; }
        }

        public Func<BasicSurfaceControl<TSurface>, Point2>
            GetControlLocationFunc {
            get { return getControlLocationFunc_; }
            set { getControlLocationFunc_ = value; }
        }

        public Func<BasicSurfaceControl<TSurface>, Point2> GetDrawLocationFunc {
            get { return getDrawLocationFunc_; }
            set { getDrawLocationFunc_ = value; }
        }

        protected void ArrangeSizeAndLocation() {
            // コンポーネントサイズの調整
            if (autoPreferSize_) {
                Size = surface_.Size;
            }

            // コンポーネント位置の調整
            if (getControlLocationFunc_ != null) {
                Location = getControlLocationFunc_(this);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent) {
            base.OnLayout(levent);

            ArrangeSizeAndLocation();
        }

        protected override void OnPaint(PaintEventArgs e) {
            var g = e.Graphics;

            // 描画位置の決定
            if (getDrawLocationFunc_ != null) {
                surface_.Draw(g, getDrawLocationFunc_(this) + GetOffset());
            } else {
                surface_.Draw(g, Point.Empty + GetOffset());
            }
        }
            }
}