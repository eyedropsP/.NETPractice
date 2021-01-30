using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExecutionContext
{
	/// <summary>
	/// スレッドローカル
	/// 共有範囲を絞ることができる
	/// </summary>
	public class MyThreadLocal
	{
		// 1つのスレッド内からだけしか読み書きできない変数を作ることができる
		private static ThreadLocal<int> ThreadLocal = new();

		public void Execute()
		{
			// メインスレッドでThreadLocal変数に123を代入
			ThreadLocal.Value = 123;

			Console.WriteLine($"ThreadLocal_Main Value : {ThreadLocal.Value}");
			
			// 別スレッドでThreadLocal変数を書き換える
			Task.WhenAll(
				Task.Run(() =>
				{
					Console.WriteLine($"ThreadLocal_1 Value : {ThreadLocal.Value}");
					ThreadLocal.Value = 1;
					Console.WriteLine($"ThreadLocal_1 Value : {ThreadLocal.Value}");
				}),
				Task.Run(() =>
				{ 
					Console.WriteLine($"ThreadLocal_2 Value : {ThreadLocal.Value}");
					ThreadLocal.Value = 2;
					Console.WriteLine($"ThreadLocal_2 Value : {ThreadLocal.Value}");
				}),
				Task.Run(() =>
				{
					Console.WriteLine($"ThreadLocal_3 Value : {ThreadLocal.Value}");
					ThreadLocal.Value = 3;
					Console.WriteLine($"ThreadLocal_3 Value : {ThreadLocal.Value}");
				}))
				.Wait();
			
			// 変化しない
			var x = ThreadLocal.Value;
			// メインスレッドで呼び出してるから123のまま
			Console.WriteLine($"ThreadLocal_Main x : {x}");
		}
	}
}