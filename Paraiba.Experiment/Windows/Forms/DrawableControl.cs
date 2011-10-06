using System.Diagnostics;
using System.Windows.Forms;

namespace Paraiba.Windows.Forms
{
	public class DrawableControl : Control
	{
		public DrawableControl()
		{
			// �_�u���o�b�t�@�����O�̗L��
			//DoubleBuffered = true;
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			// �Ǝ��`��
			SetStyle(ControlStyles.UserPaint, true);

			// �w�i�`��̏ȗ�
			SetStyle(ControlStyles.Opaque, true);

			// �����Ȕw�i�F�̋���
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}
	}
}