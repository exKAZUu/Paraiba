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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Paraiba.Geometry;

namespace Paraiba.Windows.Forms {
    public partial class ScrollablePanel : UserControl {
        private readonly Control _padding;
        private bool _arranging;
        private MouseButtons _dragMoveButtons;
        private HorizontalLocationStyle _horizontalLocationStyle;
        private Point _lastPoint;
        private Control _panel;
        private VerticalLocationStyle _verticalLocationStyle;

        public ScrollablePanel() {
            InitializeComponent();

            _padding = new Control {
                    Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                    Location = new Point(_hScrollBar.Right, _vScrollBar.Bottom),
                    Size = new Size(_vScrollBar.Width, _hScrollBar.Height),
                    Visible = false,
            };
            Controls.Add(_padding);
        }

        public override Color BackColor {
            get { return base.BackColor; }
            set {
                base.BackColor = value;
                _padding.BackColor = value;
            }
        }

        public Control Panel {
            get { return _panel; }
            set {
                if (_panel != value) {
                    if (_panel != null) {
                        _panel.Layout -= Panel_Layout;
                        _panel.SizeChanged -= Panel_SizeChanged;
                        _panel.MouseMove -= Panel_MouseMove;
                        _panel.MouseDown -= Panel_MouseDown;
                        Controls.Remove(_panel);
                    }

                    _panel = value;
                    _panel.Location = Point.Empty;

                    if (value != null) {
                        if (Visible) {
                            ArrangeScrollBar();
                        }
                        value.Dock = DockStyle.None;
                        value.Layout += Panel_Layout;
                        value.SizeChanged += Panel_SizeChanged;
                        value.MouseMove += Panel_MouseMove;
                        value.MouseDown += Panel_MouseDown;
                        Controls.Add(value);
                    }
                }
            }
        }

        public bool HScrollBarVisible {
            get { return _hScrollBar.Visible; }
        }

        public bool VScrollBarVisible {
            get { return _vScrollBar.Visible; }
        }

        public HorizontalLocationStyle HorizontalLocationStyle {
            get { return _horizontalLocationStyle; }
            set {
                _horizontalLocationStyle = value;
                if (!_hScrollBar.Visible) {
                    _arranging = true;
                    _panel.Left = GetArrangedPanelLeft();
                    _arranging = false;
                }
            }
        }

        public VerticalLocationStyle VerticalLocationStyle {
            get { return _verticalLocationStyle; }
            set {
                _verticalLocationStyle = value;
                if (!_vScrollBar.Visible) {
                    _arranging = true;
                    _panel.Top = GetArrangedPanelTop();
                    _arranging = false;
                }
            }
        }

        public MouseButtons DragMoveButtons {
            get { return _dragMoveButtons; }
            set { _dragMoveButtons = value; }
        }

        public Size VisiblePanelSize {
            get {
                int width = Width, height = Height;
                if (_vScrollBar.Visible) {
                    width -= _vScrollBar.Width;
                }
                if (_hScrollBar.Visible) {
                    height -= _hScrollBar.Height;
                }
                return new Size(width, height);
            }
        }

        public void Centering(Point2 p) {
            var size = VisiblePanelSize;
            ArrangePanelLocation(
                    -(p.X - size.Width / 2), -(p.Y - size.Height / 2));
        }

        public void Covering(Point2 p) {
            // 左右の移動
            var left = _panel.Left;
            if (p.X < -left) {
                left = -p.X;
            } else if (p.X > _panel.Right - left) {
                left = _panel.Right - p.X;
            }

            var top = _panel.Top;
            if (p.Y < top) {
                top = -p.Y;
            } else if (p.Y > _panel.Bottom - top) {
                top = _panel.Bottom - p.Y;
            }

            ArrangePanelLocation(left, top);
        }

        public void Covering(Rectangle rect) {
            // 左右の移動
            var left = _panel.Left;
            if (rect.Left < -left) {
                left = -rect.Left;
            } else if (rect.Right > _panel.Right - left) {
                left = _panel.Right - rect.Right;
            }

            var top = _panel.Top;
            if (rect.Top < top) {
                top = -rect.Top;
            } else if (rect.Bottom > _panel.Bottom - top) {
                top = _panel.Bottom - rect.Bottom;
            }

            ArrangePanelLocation(left, top);
        }

        private int GetArrangedPanelLeft() {
            switch (_horizontalLocationStyle) {
            case HorizontalLocationStyle.Left:
                return 0;
            case HorizontalLocationStyle.Center:
                return (Width - _panel.Width) / 2;
            case HorizontalLocationStyle.Right:
                return Width - _panel.Width;
            default:
                Debug.Assert(false);
                break;
            }
            return 0;
        }

        private int GetArrangedPanelTop() {
            switch (_verticalLocationStyle) {
            case VerticalLocationStyle.Top:
                return 0;
            case VerticalLocationStyle.Center:
                return (Height - _panel.Height) / 2;
            case VerticalLocationStyle.Bottom:
                return Height - _panel.Height;
            default:
                Debug.Assert(false);
                break;
            }
            return 0;
        }

        private void ArrangePanelLocation(int x, int y) {
            if (_arranging) {
                return;
            }

            _arranging = true;

            if (_hScrollBar.Visible) {
                x = XMath.Center(
                        x, -(_hScrollBar.Maximum + 1 - _hScrollBar.LargeChange),
                        0);
                _hScrollBar.Value = -x;
            } else {
                x = GetArrangedPanelLeft();
                _hScrollBar.Value = 0;
            }

            if (_vScrollBar.Visible) {
                y = XMath.Center(
                        y, -(_vScrollBar.Maximum + 1 - _vScrollBar.LargeChange),
                        0);
                _vScrollBar.Value = -y;
            } else {
                y = GetArrangedPanelTop();
                _vScrollBar.Value = 0;
            }

            _panel.Location = new Point(x, y);

            _arranging = false;
        }

        private void ArrangeScrollBar() {
            var hScrollBar = _hScrollBar;
            var vScrollBar = _vScrollBar;

            var visibleWidth = Width;
            var visibleHeight = Height;

            var panelWidth = _panel.Width;
            var panelHeight = _panel.Height;

            // スクロールバーの表示・非表示の設定
            if (panelWidth > visibleWidth) {
                visibleHeight -= hScrollBar.Height;
                hScrollBar.Visible = true;

                if (panelHeight > visibleHeight) {
                    visibleWidth -= vScrollBar.Width;
                    vScrollBar.Visible = true;
                    _padding.Visible = true;
                } else {
                    vScrollBar.Visible = false;
                    _padding.Visible = false;
                }
            } else if (panelHeight > visibleHeight) {
                visibleWidth -= vScrollBar.Width;
                vScrollBar.Visible = true;

                if (panelWidth > visibleWidth) {
                    visibleHeight -= hScrollBar.Height;
                    hScrollBar.Visible = true;
                    _padding.Visible = true;
                } else {
                    hScrollBar.Visible = false;
                    _padding.Visible = false;
                }
            } else {
                vScrollBar.Visible = false;
                hScrollBar.Visible = false;
                _padding.Visible = false;
            }

            // 水平スクロールバーの値とパネルの位置の設定
            if (hScrollBar.Visible) {
                hScrollBar.SetBounds(
                        0, Height - hScrollBar.Height, visibleWidth, 0,
                        BoundsSpecified.Location | BoundsSpecified.Width);

                // スクロール量の調整
                hScrollBar.LargeChange = XMath.Max(0, visibleWidth);
                // スクロールバーの最大値の調整(MaxValue + LargeChange == Maximum + 1)
                hScrollBar.Maximum = panelWidth - 1;
            }

            // 垂直スクロールバーの値とパネルの位置の設定
            if (vScrollBar.Visible) {
                vScrollBar.SetBounds(
                        Width - vScrollBar.Width, 0, 0, visibleHeight,
                        BoundsSpecified.Location | BoundsSpecified.Height);

                // スクロール量の調整
                vScrollBar.LargeChange = XMath.Max(0, visibleHeight);
                // スクロールバーの最大値の調整(MaxValue + LargeChange == Maximum + 1)
                vScrollBar.Maximum = panelHeight - 1;
            }

            // パネルの位置調整
            ArrangePanelLocation(_panel.Left, _panel.Top);
        }

        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);

            if (Visible && _panel != null) {
                ArrangeScrollBar();
            }
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);

            if (_panel != null) {
                ArrangeScrollBar();
            }
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e) {
            // パネルの位置調整
            ArrangePanelLocation(-_hScrollBar.Value, -_vScrollBar.Value);
        }

        private void Panel_Layout(object sender, LayoutEventArgs e) {
            // パネルの位置調整
            ArrangePanelLocation(_panel.Left, _panel.Top);
        }

        private void Panel_SizeChanged(object sender, EventArgs e) {
            // スクロールバーのサイズ調整
            ArrangeScrollBar();
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e) {
            // マウスのボタンを押した位置を記憶
            _lastPoint = Cursor.Position;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.None && e.Button == _dragMoveButtons) {
                var currentPoint = Cursor.Position;
                // マウスの移動量の分だけマップをスクロール
                var dx = currentPoint.X - _lastPoint.X;
                var dy = currentPoint.Y - _lastPoint.Y;
                // パネルの位置調整
                ArrangePanelLocation(_panel.Left + dx, _panel.Top + dy);
                _lastPoint = currentPoint;
            }
        }
    }
}