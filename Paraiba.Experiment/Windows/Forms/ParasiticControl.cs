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
using Paraiba.Geometry;

namespace Paraiba.Windows.Forms {
	public partial class ParasiticControl : UserControl {
		private bool _enableMouseUp;
		private bool _entered;
		private MouseButtons _lastMouseDownButton;
		private Control _parent;
		private bool _visible;

		public ParasiticControl() {
			InitializeComponent();

			// 基底クラスのプロパティでは非表示に設定する
			base.Visible = false;
			// 親コンポーネントに寄生した描画を有効にする
			_visible = true;
		}

		public new bool Visible {
			get { return _visible; }
			set {
				_visible = value;
				if (!value) {
					// 非表示の際はマウス操作に関するパラメータをリセットする
					_enableMouseUp = false;
					_entered = false;
					_lastMouseDownButton = MouseButtons.None;
				}
			}
		}

		protected override sealed void OnVisibleChanged(EventArgs e) {
			base.OnVisibleChanged(e);

			// 基底クラスのプロパティでは常に非表示に設定する
			base.Visible = false;
		}

		protected override void OnParentChanged(EventArgs e) {
			base.OnParentChanged(e);

			// 以前の親コンポーネントに設定したデリゲートを取り除く
			{
				var parent = _parent;

				if (parent != null) {
					parent.MouseMove -= ParentControl_MouseMove;
					parent.MouseDown -= ParentControl_MouseDown;
					parent.MouseWheel -= ParentControl_MouseWheel;
					parent.Click -= ParentControl_Click;
					parent.MouseClick -= ParentControl_MouseClick;
					parent.DoubleClick -= ParentControl_DoubleClick;
					parent.MouseUp -= ParentControl_MouseUp;
					parent.MouseLeave -= ParentControl_MouseLeave;
					parent.Paint -= ParentControl_Paint;
				}
			}

			// 新しい親コンポーネントにデリゲートを設定する
			{
				var parent = Parent;
				_parent = parent;

				if (parent != null) {
					parent.MouseMove += ParentControl_MouseMove;
					parent.MouseDown += ParentControl_MouseDown;
					parent.MouseWheel += ParentControl_MouseWheel;
					parent.Click += ParentControl_Click;
					parent.MouseClick += ParentControl_MouseClick;
					parent.DoubleClick += ParentControl_DoubleClick;
					parent.MouseUp += ParentControl_MouseUp;
					parent.MouseLeave += ParentControl_MouseLeave;
					parent.Paint += ParentControl_Paint;
				}
			}
		}

		protected Vector2 GetOffset() {
			return new Vector2(Left, Top);
		}

		protected Point GetParentLocation(int x, int y) {
			return new Point(x + Left, y + Top);
		}

		protected Rectangle GetParentRectangle(
				int x, int y, int width, int height) {
			return new Rectangle(x + Left, y + Top, width, height);
		}

		private void ParentControl_MouseMove(object sender, MouseEventArgs e) {
			if (!_visible) {
				return;
			}

			int x = e.X - Left, y = e.Y - Top;
			if (0 <= x && x < Width && 0 <= y && y < Height) {
				if (!_entered) {
					_entered = true;
					OnMouseEnter(e);
				}
				OnMouseMove(
						new MouseEventArgs(e.Button, e.Clicks, x, y, e.Delta));
			} else if (_entered) {
				// 領域から抜けた場合、（既に押されたボタン以外の）MouseUpを無効化する
				_entered = false;
				_enableMouseUp = false;
				OnMouseLeave(e);
			}
		}

		private void ParentControl_MouseDown(object sender, MouseEventArgs e) {
			if (!_visible) {
				return;
			}

			if (_entered) {
				// 一度MouseDownが呼ばれるまでは、MouseUpを受け付けない
				_enableMouseUp = true;
				_lastMouseDownButton |= e.Button;
				OnMouseDown(
						new MouseEventArgs(
								e.Button, e.Clicks, e.X - Left, e.Y - Top,
								e.Delta));
			} else {
				// 一度外でMouseDownが呼ばれた場合、既に押されているボタンのMouseUpも無効化
				_lastMouseDownButton = MouseButtons.None;
			}
		}

		private void ParentControl_MouseWheel(object sender, MouseEventArgs e) {
			if (!_visible) {
				return;
			}

			if (_entered) {
				OnMouseWheel(
						new MouseEventArgs(
								e.Button, e.Clicks, e.X - Left, e.Y - Top,
								e.Delta));
			}
		}

		private void ParentControl_Click(object sender, EventArgs e) {
			if (!_visible) {
				return;
			}

			if (_entered) {
				OnClick(e);
			}
		}

		private void ParentControl_MouseClick(object sender, MouseEventArgs e) {
			if (!_visible) {
				return;
			}

			if (_entered) {
				OnMouseClick(
						new MouseEventArgs(
								e.Button, e.Clicks, e.X - Left, e.Y - Top,
								e.Delta));
			}
		}

		private void ParentControl_DoubleClick(object sender, EventArgs e) {
			if (!_visible) {
				return;
			}

			if (_entered) {
				OnDoubleClick(e);
			}
		}

		private void ParentControl_MouseUp(object sender, MouseEventArgs e) {
			if (!_visible) {
				return;
			}

			if (_enableMouseUp || (_lastMouseDownButton & e.Button) != 0) {
				_lastMouseDownButton &= ~e.Button;
				OnMouseUp(
						new MouseEventArgs(
								e.Button, e.Clicks, e.X - Left, e.Y - Top,
								e.Delta));
			}
		}

		private void ParentControl_MouseLeave(object sender, EventArgs e) {
			if (!_visible) {
				return;
			}

			if (_entered) {
				_entered = false;
				OnMouseLeave(e);
			}
		}

		protected virtual void ParentControl_Paint(
				object sender, PaintEventArgs e) {
			if (!_visible) {
				return;
			}

			var rect = e.ClipRectangle;
			rect.Intersect(Bounds);
			if (rect.Width > 0 && rect.Height > 0) {
				rect.Offset(-Left, -Top);
				OnPaint(new PaintEventArgs(e.Graphics, rect));
			}
		}

		protected void InvalidateEx() {
			if (!_visible) {
				return;
			}

			if (Parent != null) {
				Parent.Invalidate(Bounds, true);
			}
		}

		protected override void OnPaint(PaintEventArgs e) {}

		protected override sealed void OnPaintBackground(PaintEventArgs e) {}
	}
}