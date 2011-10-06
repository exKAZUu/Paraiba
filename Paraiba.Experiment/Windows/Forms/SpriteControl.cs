using Paraiba.Drawing.Surfaces.Sprites;

namespace Paraiba.Windows.Forms
{
	public class SpriteControl : BasicSpriteControl<Sprite>
	{
		public SpriteControl(Sprite sprite)
			: base(sprite)
		{
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			ArrangeSizeAndLocation();

			base.OnPaint(e);
		}
	}
}