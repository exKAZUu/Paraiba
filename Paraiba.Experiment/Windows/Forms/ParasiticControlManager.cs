//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;

//namespace Paraiba.Windows.Forms
//{
//    public class ParasiticControlManager : Control
//    {
//        private List<ParasiticControl> _controls;
//        private Control _parent;

//        public ParasiticControlManager()
//        {
//            _controls = new List<ParasiticControl>();

//            Visible = false;
//        }

//        public void Add(ParasiticControl control)
//        {
//            _controls.Add(control);
//        }

//        public void Clear()
//        {
//            _controls.Clear();
//        }

//        public void Remove(ParasiticControl control)
//        {
//            _controls.Remove(control);
//        }

//        public void RemoveAt(int n)
//        {
//            _controls.RemoveAt(n);
//        }

//        protected sealed override void OnVisibleChanged(EventArgs e)
//        {
//            base.OnVisibleChanged(e);

//            Visible = false;
//        }

//        protected override void OnParentChanged(EventArgs e)
//        {
//            base.OnParentChanged(e);

//            if (Parent != null)
//            {
//                var parent = Parent;
//                _parent = parent;

//                parent.MouseMove += ParentControl_MouseMove;
//                parent.MouseDown += ParentControl_MouseDown;
//                parent.MouseWheel += ParentControl_MouseWheel;
//                parent.Click += ParentControl_Click;
//                parent.MouseClick += ParentControl_MouseClick;
//                parent.DoubleClick += ParentControl_DoubleClick;
//                parent.MouseUp += ParentControl_MouseUp;
//                parent.MouseLeave += ParentControl_MouseLeave;
//                parent.Paint += ParentControl_Paint;
//            }
//            else if (_parent != null)
//            {
//                var parent = _parent;
//                _parent = null;

//                parent.MouseMove -= ParentControl_MouseMove;
//                parent.MouseDown -= ParentControl_MouseDown;
//                parent.MouseWheel -= ParentControl_MouseWheel;
//                parent.Click -= ParentControl_Click;
//                parent.MouseClick -= ParentControl_MouseClick;
//                parent.DoubleClick -= ParentControl_DoubleClick;
//                parent.MouseUp -= ParentControl_MouseUp;
//                parent.MouseLeave -= ParentControl_MouseLeave;
//                parent.Paint -= ParentControl_Paint;
//            }
//        }

//        private void ParentControl_MouseMove(object sender, MouseEventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_MouseMove(sender, e))
//                    break;
//            }
//        }

//        private void ParentControl_MouseDown(object sender, MouseEventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_MouseDown(sender, e))
//                    break;
//            }
//        }

//        private void ParentControl_MouseWheel(object sender, MouseEventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_MouseWheel(sender, e))
//                    break;
//            }
//        }

//        private void ParentControl_Click(object sender, EventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_Click(sender, e))
//                    break;
//            }
//        }

//        private void ParentControl_MouseClick(object sender, MouseEventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_MouseClick(sender, e))
//                    break;
//            }
//        }

//        private void ParentControl_DoubleClick(object sender, EventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_DoubleClick(sender, e))
//                    break;
//            }
//        }

//        private void ParentControl_MouseUp(object sender, MouseEventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_MouseUp(sender, e))
//                    break;
//            }
//        }

//        private void ParentControl_MouseLeave(object sender, EventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                if (control.ParentControl_MouseLeave(sender, e))
//                    break;
//            }
//        }

//        protected virtual void ParentControl_Paint(object sender, PaintEventArgs e)
//        {
//            foreach (var control in _controls)
//            {
//                control.ParentControl_Paint(sender, e);
//            }
//        }
//    }
//}
