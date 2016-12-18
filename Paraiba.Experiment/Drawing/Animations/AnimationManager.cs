#region License

// Copyright (C) 2011-2016 Kazunori Sakamoto
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Paraiba.Core;

namespace Paraiba.Drawing.Animations {
    public class AnimationManager<T> : IEnumerable<T>
            where T : IAnimation {
        private readonly LinkedList<AnimationStruct> _animations =
                new LinkedList<AnimationStruct>();

        public void Add(T anime) {
            _animations.AddLast(new AnimationStruct(anime));
        }

        public void Add(T anime, Action endEvent) {
            _animations.AddLast(
                new AnimationStruct(
                    anime, (_) => endEvent.InvokeIfNotNull()));
        }

        public void Add(T anime, Action<T> endEvent) {
            _animations.AddLast(new AnimationStruct(anime, endEvent));
        }

        public bool Elapse(float time) {
            var next = _animations.First;
            if (next == null) {
                return false;
            }

            var requiredRefresh = false;
            do {
                var node = next;
                next = next.Next;
                requiredRefresh |= node.Value.Animation.Elapse(time);
                if (node.Value.Animation.Ended) {
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

        public void Remove(T anime) {
            _animations.Remove(new AnimationStruct(anime));
        }

        #region IEnumerable<TValue> メンバ

        public IEnumerator<T> GetEnumerator() {
            return
                    _animations.Select(animeInfo => animeInfo.Animation).
                            GetEnumerator();
        }

        #endregion

        #region IEnumerable メンバ

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

        #region Nested type: AnimationStruct

        private struct AnimationStruct : IEquatable<AnimationStruct> {
            public readonly T Animation;
            public readonly Action<T> EndEvent;

            public AnimationStruct(T anime) {
                Animation = anime;
                EndEvent = null;
            }

            public AnimationStruct(T anime, Action<T> endEvent) {
                Animation = anime;
                EndEvent = endEvent;
            }

            #region IEquatable<AnimationManager<T>.AnimationStruct> Members

            public bool Equals(AnimationStruct other) {
                return Animation.Equals(other.Animation);
            }

            #endregion
        }

        #endregion
    }
}