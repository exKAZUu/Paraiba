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

using Paraiba.Drawing.Surfaces;

namespace Paraiba.Drawing.Animations.Surfaces {
    /// <summary>
    ///   アニメーションする（時間経過とともに変化する）表示可能オブジェクトを表します。
    /// </summary>
    public abstract class AnimationSurface : Surface, IAnimation {
        #region IAnimation Members

        public abstract bool Ended { get; }
        public abstract float ExcessTime { get; }
        public abstract bool Elapse(float time);
        public abstract void Reset();

        #endregion
    }
}