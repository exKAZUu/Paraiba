using System.Diagnostics;
using System.Windows.Forms;

namespace Paraiba.Windows.Forms
{
	public class DrawableControl : Control
	{
		public DrawableControl()
		{
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