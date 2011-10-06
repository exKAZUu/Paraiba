using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Paraiba.Drawing.Surfaces
{
	public class NullSurface : Surface
	{
		public override Size Size {
			get { return new Size(0, 0); }
		}

		public override void Draw(Graphics g, int x, int y) {
		}
	}
}
