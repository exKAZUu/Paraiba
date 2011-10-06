using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces
{
	public class StaticAnimation : AnimationSurface
	{
		private readonly Surface _surface;

		public StaticAnimation(Surface surface)
		{
			Contract.Requires(surface != null);

			_surface = surface;
		}

		#region AnimationSurface Members

		public override Size Size
		{
			get { return _surface.Size; }
		}

		public override void Draw(Graphics g, int x, int y)
		{
			_surface.Draw(g, x, y);
		}

		public override Image GetImage()
		{
			return _surface.GetImage();
		}

		#endregion

		#region IAnimation メンバ

		public override bool Ended
		{
			get { return false; }
		}

		public override float ExcessTime
		{
			get { return float.NegativeInfinity; }
		}

		public override bool Elapse(float time)
		{
			return false;
		}

		public override void Reset()
		{
		}

		#endregion
	}
}