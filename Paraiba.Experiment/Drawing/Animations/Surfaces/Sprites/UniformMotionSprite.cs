#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace Paraiba.Drawing.Animations.Surfaces.Sprites {
	/// <summary>
	///   等速移動を行う配置可能でかつ表示可能なアニメーションオブジェクトを表します。
	/// </summary>
	public class UniformMotionSprite : AnimationSprite {
		private readonly AnimationSurface _animationSurface;
		private readonly Vector2 _diffVector;
		private readonly float _totalTime;
		private float _lapseTime;
		private Point2 _location;
		private Point2 _startLocation;

		public UniformMotionSprite(
				Surface surface, Point2 startLocation, Point2 endLocation,
				float totalTime)
				: this(
						new StaticAnimation(surface), startLocation, endLocation,
						totalTime) {}

		public UniformMotionSprite(
				AnimationSurface animationSurface, Point2 startLocation,
				Point2 endLocation, float totalTime) {
			Contract.Requires(animationSurface != null);

			_animationSurface = animationSurface;
			_startLocation = startLocation;
			_location = startLocation;
			_diffVector = endLocation - startLocation;
			_totalTime = totalTime;
		}

		public UniformMotionSprite(
				Surface surface, Point2 startLocation, Vector2 velocity,
				float totalTime)
				: this(
						new StaticAnimation(surface), startLocation,
						startLocation + (Vector2)(velocity * totalTime),
						totalTime) {}

		public UniformMotionSprite(
				AnimationSurface animationSurface, Point2 startLocation,
				Vector2 velocity, float totalTime)
				: this(
						animationSurface, startLocation,
						startLocation + (Vector2)(velocity * totalTime),
						totalTime) {}

		public UniformMotionSprite(
				Surface surface, Point2 startLocation, Vector2F velocity,
				float totalTime)
				: this(
						new StaticAnimation(surface), startLocation,
						startLocation + (Vector2)(velocity * totalTime),
						totalTime) {}

		public UniformMotionSprite(
				AnimationSurface animationSurface, Point2 startLocation,
				Vector2F velocity, float totalTime)
				: this(
						animationSurface, startLocation,
						startLocation + (Vector2)(velocity * totalTime),
						totalTime) {}

		public UniformMotionSprite(
				Surface surface, Point2 startLocation, Point2 endLocation,
				float speedOrTotalTime, bool isSpeed)
				: this(
						new StaticAnimation(surface), startLocation, endLocation,
						isSpeed
								? (float)(endLocation - startLocation).Length
										/ speedOrTotalTime
								: speedOrTotalTime) {}

		public UniformMotionSprite(
				AnimationSurface animationSurface, Point2 startLocation,
				Point2 endLocation, float speedOrTotalTime, bool isSpeed)
				: this(
						animationSurface, startLocation, endLocation,
						isSpeed
								? (float)(endLocation - startLocation).Length
										/ speedOrTotalTime
								: speedOrTotalTime) {}

		#region Sprite メンバ

		public Point2 StartLocation {
			get { return _startLocation; }
			set { _startLocation = value; }
		}

		public override Surface CurrentSurface {
			get { return _animationSurface; }
		}

		public override Point2 Location {
			get { return _location; }
			set {
				_location = value;
				_startLocation = value
						-
						(Vector2)
								(_diffVector * (_lapseTime / _totalTime));
			}
		}

		public override Size Size {
			get { return _animationSurface.Size; }
		}

		public override void Draw(Graphics g) {
			_animationSurface.Draw(g, _location);
		}

		public override void Draw(Graphics g, Vector2 offset) {
			_animationSurface.Draw(g, _location + offset);
		}

		public override void Draw(
				Graphics g, Func<Point2, Size, Point2> coordinateTransformer) {
			_animationSurface.Draw(
					g, coordinateTransformer(_location, _animationSurface.Size));
		}

		public override void Draw(
				Graphics g, Vector2 offset,
				Func<Point2, Size, Point2> coordinateTransformer) {
			_animationSurface.Draw(
					g,
					coordinateTransformer(
							_location + offset, _animationSurface.Size));
		}

		#endregion

		#region IAnimation メンバ

		public override float ExcessTime {
			get { return _lapseTime - _totalTime; }
		}

		public override bool Ended {
			get { return _lapseTime >= _totalTime; }
		}

		public override bool Elapse(float time) {
			_animationSurface.Elapse(time);

			_lapseTime += time;
			if (_lapseTime < _totalTime) {
				// 毎回合計値と除算することで誤差を防ぐ
				_location = _startLocation
						+ (Vector2)(_diffVector * (_lapseTime / _totalTime));
			} else {
				_location = _startLocation + _diffVector;
			}
			return true;
		}

		public override void Reset() {
			_lapseTime = 0;
			_location = _startLocation;
		}

		#endregion
	}
}