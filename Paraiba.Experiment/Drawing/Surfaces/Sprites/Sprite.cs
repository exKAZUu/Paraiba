﻿using System;
using System.Drawing;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Surfaces.Sprites
{
	/// <summary>
	/// 配置可能でかつ表示可能なオブジェクトを表します。
	/// </summary>
	public abstract class Sprite {
		/// <summary>
		///  現在、表示で利用される Surface を取得します。
		///  </summary>
		public abstract Surface CurrentSurface { get; }

		/// <summary>
		///  オブジェクトの位置を取得または設定します。
		///  </summary>
		public abstract Point2 Location { get; set; }

		/// <summary>
		///  オブジェクトの位置のX座標を取得または設定します。
		///  </summary>
		public int X {
			get { return Location.X; }
			set { Location = new Point2(value, Y); }
		}

		/// <summary>
		///  オブジェクトの位置のY座標を取得または設定します。
		///  </summary>
		public int Y {
			get { return Location.Y; }
			set { Location = new Point2(X, value); }
		}

		/// <summary>
		///  オブジェクトの縦幅のサイズを取得します。
		///  </summary>
		public int Height {
			get { return Size.Height; }
		}

		/// <summary>
		///  オブジェクトの横幅のサイズを取得します。
		///  </summary>
		public int Width {
			get { return Size.Width; }
		}

		/// <summary>
		///  オブジェクトの縦横のサイズを取得します。
		///  </summary>
		public abstract Size Size { get; }

		/// <summary>
		///  オブジェクトを描画します。
		///  </summary><param name="g">描画で利用する Graphics</param>
		public abstract void Draw(Graphics g);

		/// <summary>
		///  オブジェクトを現在の位置からずらして描画します。
		///  </summary><param name="g">描画で利用する Graphics</param><param name="offset">現在の位置から描画位置への差分ベクトル</param>
		public abstract void Draw(Graphics g, Vector2 offset);

		/// <summary>
		///  オブジェクトを描画します。
		///  </summary><param name="g">描画で利用する Graphics</param><param name="coordinateTransformer"></param>
		public abstract void Draw(Graphics g, Func<Point2, Size, Point2> coordinateTransformer);

		/// <summary>
		///  オブジェクトを描画します。
		///  </summary><param name="g">描画で利用する Graphics</param><param name="offset">現在の位置から描画位置への差分ベクトル</param><param name="coordinateTransformer"></param>
		public abstract void Draw(Graphics g, Vector2 offset, Func<Point2, Size, Point2> coordinateTransformer);
	}
}