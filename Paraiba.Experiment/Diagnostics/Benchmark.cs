using System;
using System.Diagnostics;

namespace Paraiba.Diagnostics
{
	public class Benchmark : IDisposable
	{
		private static int count = 1;
		private static long sumTime;

		private readonly string _name;
		private readonly Stopwatch _watch;

		public Benchmark()
		{
			_name = null;
			_watch = new Stopwatch();
			_watch.Start();
		}

		public Benchmark(string name)
		{
			_name = name;
			_watch = new Stopwatch();
			_watch.Start();
		}

		#region IDisposable Members

		///<summary>
		///アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
		///</summary>
		///<filterpriority>2</filterpriority>
		public void Dispose()
		{
			_watch.Stop();
			var time = _watch.ElapsedTicks;
			sumTime += time;

			if (_name != null)
			{
				Console.WriteLine("[" + count++ + "] " + _name + " : " + time + " [ms], " + sumTime + " [ms]");
			}
			else
			{
				Console.WriteLine("[" + count++ + "] " + time + " [ms], " + sumTime + " [ms]");
			}
		}

		#endregion
	}
}