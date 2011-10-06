using Paraiba.Drawing.Surfaces;

namespace Paraiba.Windows.Forms
{
	public class SurfaceControl : BasicSurfaceControl<Surface>
	{
		public SurfaceControl(Surface surface)
			: base(surface)
		{
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			ArrangeSizeAndLocation();

			base.OnPaint(e);
		}
	}
}