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

namespace Paraiba.Drawing.Animations {
	/// <summary>
	///   時間経過のロジックを備えたオブジェクトを表します。
	/// </summary>
	public interface IAnimation {
		/// <summary>
		///   アニメーションが終了したかどうかを示す値を取得します。
		/// </summary>
		bool Ended { get; }

		/// <summary>
		///   アニメーション終了からの経過時間（終了前は負の値）を取得します。
		/// </summary>
		float ExcessTime { get; }

		/// <summary>
		///   指定した経過時間の分、アニメーションを進めます。
		/// </summary>
		/// <param name="time"> 経過した時間 </param>
		/// <returns> アニメーションが変化したかどうか </returns>
		bool Elapse(float time);

		/// <summary>
		///   アニメーションを初期状態に戻す
		/// </summary>
		void Reset();
	}
}