namespace Paraiba.Drawing.Animations
{
	/// <summary>
	/// 時間経過のロジックを備えたオブジェクトを表します。
	/// </summary>
	public interface IAnimation
	{
		/// <summary>
		/// アニメーションが終了したかどうかを示す値を取得します。
		/// </summary>
		bool Ended { get; }

		/// <summary>
		/// アニメーション終了からの経過時間（終了前は負の値）を取得します。
		/// </summary>
		float ExcessTime { get; }

		/// <summary>
		/// 指定した経過時間の分、アニメーションを進めます。
		/// </summary>
		/// <param name="time">経過した時間</param>
		/// <returns>アニメーションが変化したかどうか</returns>
		bool Elapse(float time);

		/// <summary>
		/// アニメーションを初期状態に戻す
		/// </summary>
		void Reset();
	}
}