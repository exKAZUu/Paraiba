using Paraiba.Utility;

namespace Paraiba.Geometry
{
	public class StraightLine
	{
		// ax + by + c = 0

		internal float a_, b_, c_;
		internal IntervalF xInterval_, yInterval_;

		public StraightLine(Point2F p1, Point2F p2)
		{
			float a = p1.Y - p2.Y, b = p2.X - p1.X;
			a_ = a;
			b_ = b;
			c_ = -(a * p1.X + b * p1.Y);
			xInterval_ = new IntervalF(p1.X, p2.X);
			yInterval_ = new IntervalF(p1.Y, p2.Y);
		}

		public StraightLine(float a, float b, float c, IntervalF xInterval, IntervalF yInterval)
		{
			a_ = a;
			b_ = b;
			c_ = c;
			xInterval_ = xInterval;
			yInterval_ = yInterval;
		}

		public StraightLine(float a, float b, float c, IntervalF xInterval)
			: this(a, b, c, xInterval, new IntervalF((-a * xInterval._min - c) / b, (-a * xInterval._max - c) / b))
		{
		}

		public StraightLine(float a, float b, float c, IntervalF interval, bool isXRange)
			: this(a, b, c,
				isXRange ? interval : new IntervalF((-b * interval._min - c) / a, (-b * interval._max - c) / a),
				isXRange == false ? interval : new IntervalF((-a * interval._min - c) / b, (-a * interval._max - c) / b))
		{
		}

		public bool isOverlap(Point2F p)
		{
			return a_ * p.X + b_ * p.Y + c_ == 0 &&
				xInterval_.InRange(p.X) && yInterval_.InRange(p.Y);
		}

		public bool isOverlap(StraightLine line)
		{
			float d = a_ * line.b_ - line.a_ * b_;

			if (d != 0)
			{
				// 交点が存在する
				float x = (-line.b_ * c_ + b_ * line.c_) / d;
				if (!xInterval_.InRange(x) || !line.xInterval_.InRange(x))
					return false;

				float y = (+line.a_ * c_ - a_ * line.c_) / d;
				return yInterval_.InRange(y) && line.yInterval_.InRange(y);
			}
			else
			{
				// 平行である（交点が存在しない）
				return false;
			}
		}
	}
}