using System;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces
{
	/// <summary>
	/// 表示の基本ロジックを実装した表示可能なスプライトを表します。
	/// </summary>
	public abstract class Surface : ICloneable
	{
		/// <summary>
		/// 現在のオブジェクトの縦幅のサイズを取得します。
		/// </summary>
		public int Height {
			get { return Size.Height; }
		}
		
		/// <summary>
		/// 現在のオブジェクトの横幅のサイズを取得します。
		/// </summary>
		public int Width {
			get { return Size.Width; }
		}

		/// <summary>
		/// 現在のオブジェクトの縦横のサイズを取得します。
		/// </summary>
		public abstract Size Size { get; }

		/// <summary>
		/// オブジェクトの左上原点が指定した位置に来るように描画します。
		/// </summary>
		/// <param name="g">描画で利用する Graphics</param>
		/// <param name="p">描画位置</param>
		public void Draw(Graphics g, Point p)
		{
			Draw(g, p.X, p.Y);
		}

		/// <summary>
		/// オブジェクトの左上原点が指定した位置に来るように描画します。
		/// </summary>
		/// <param name="g">描画で利用する Graphics</param>
		/// <param name="p">描画位置</param>
		public void Draw(Graphics g, Point2 p)
		{
			Draw(g, p.X, p.Y);
		}

		/// <summary>
		/// オブジェクトの左上原点が指定した位置に来るように描画します。
		/// </summary>
		/// <param name="g">描画で利用する Graphics</param>
		/// <param name="x">描画位置のX座標</param>
		/// <param name="y">描画位置のY座標</param>
		public abstract void Draw(Graphics g, int x, int y);

		/// <summary>
		/// 現在のオブジェクトが描画された Image インスタンスを取得します。
		/// </summary>
		/// <returns>現在のオブジェクトが描画された Image インスタンス</returns>
		public virtual Image GetImage()
		{
			var bmp = new Bitmap(Width, Height);
			using (var g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.Transparent);
				Draw(g, 0, 0);
			}
			return bmp;
		}

		#region ICloneable メンバ

		public object Clone()
		{
			return MemberwiseClone();
		}

		#endregion
	}
}