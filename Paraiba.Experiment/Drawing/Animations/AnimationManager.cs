using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Paraiba.Core;

namespace Paraiba.Drawing.Animations
{
	public class AnimationManager<T> : IEnumerable<T>
		where T : IAnimation
	{
		private readonly LinkedList<AnimationStruct> _animations = new LinkedList<AnimationStruct>();

		public void Add(T anime)
		{
			_animations.AddLast(new AnimationStruct(anime));
		}

		public void Add(T anime, Action endEvent)
		{
			_animations.AddLast(new AnimationStruct(anime, (_) => endEvent.InvokeIfNotNull()));
		}

		public void Add(T anime, Action<T> endEvent)
		{
			_animations.AddLast(new AnimationStruct(anime, endEvent));
		}

		public bool Elapse(float time)
		{
			var next = _animations.First;
			if (next == null)
				return false;

			var requiredRefresh = false;
			do
			{
				var node = next;
				next = next.Next;
				requiredRefresh |= node.Value.Animation.Elapse(time);
				if (node.Value.Animation.Ended)
				{
					// アニメーション終了を通達
					node.Value.EndEvent.InvokeIfNotNull(node.Value.Animation);
					// リストから取り除く
					_animations.Remove(node);
					// アニメーションが終了したので、リフレッシュの必要性が生じる
					requiredRefresh = true;
				}
			} while (next != null);

			return requiredRefresh;
		}

		public void Remove(T anime)
		{
			_animations.Remove(new AnimationStruct(anime));
		}

		#region IEnumerable<TValue> メンバ

		public IEnumerator<T> GetEnumerator()
		{
			return _animations.Select(animeInfo => animeInfo.Animation).GetEnumerator();
		}

		#endregion

		#region IEnumerable メンバ

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Nested type: AnimationStruct

		private struct AnimationStruct : IEquatable<AnimationStruct>
		{
			public readonly T Animation;
			public readonly Action<T> EndEvent;

			public AnimationStruct(T anime)
			{
				Animation = anime;
				EndEvent = null;
			}

			public AnimationStruct(T anime, Action<T> endEvent)
			{
				Animation = anime;
				EndEvent = endEvent;
			}

			#region IEquatable<AnimationManager<TValue>.AnimationStruct> Members

			public bool Equals(AnimationStruct other)
			{
				return Animation.Equals(other.Animation);
			}

			#endregion
		}

		#endregion
	}
}