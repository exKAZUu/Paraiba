using System;
using System.Windows.Forms;
using Paraiba.Drawing.Surfaces.Sprites;
using Paraiba.Geometry;

namespace Paraiba.Windows.Forms
{
	public class BasicSpriteControl<TSprite> : ParasiticControl
		where TSprite : Sprite
	{
		protected bool autoPreferSize_;
		protected Point2 basingPoint_;
		protected Func<BasicSpriteControl<TSprite>, Point2> getControlLocationFunc_;
		protected Func<BasicSpriteControl<TSprite>, Point2> getDrawOffsetFunc_;
		protected TSprite sprite_;

		protected BasicSpriteControl(TSprite sprite)
		{
			sprite_ = sprite;

			ArrangeSizeAndLocation();
		}

		public TSprite Sprite
		{
			get { return sprite_; }
			set { sprite_ = value; }
		}

		public bool AutoPreferSize
		{
			get { return autoPreferSize_; }
			set { autoPreferSize_ = value; }
		}

		public Func<BasicSpriteControl<TSprite>, Point2> GetControlLocationFunc
		{
			get { return getControlLocationFunc_; }
			set { getControlLocationFunc_ = value; }
		}

		public Func<BasicSpriteControl<TSprite>, Point2> GetDrawOffsetFunc
		{
			get { return getDrawOffsetFunc_; }
			set { getDrawOffsetFunc_ = value; }
		}

		protected void ArrangeSizeAndLocation()
		{
			var sprite = sprite_;

			// コンポーネントサイズの調整
			if (autoPreferSize_)
			{
				Size = sprite.Size;
			}

			// コンポーネント位置の調整
			Location = getControlLocationFunc_ != null ?
			                                           	getControlLocationFunc_(this) : sprite.Location;
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);

			ArrangeSizeAndLocation();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			var g = e.Graphics;
			var sprite = sprite_;

			// 描画位置の決定
			if (getDrawOffsetFunc_ != null)
			{
				sprite.Draw(g, -(Vector2)sprite.Location + (Vector2)getDrawOffsetFunc_(this) + GetOffset());
			}
			else
			{
				sprite.Draw(g, -(Vector2)sprite.Location + GetOffset());
			}
		}
	}
}