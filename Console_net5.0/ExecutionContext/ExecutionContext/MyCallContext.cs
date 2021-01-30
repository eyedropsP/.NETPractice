using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace ExecutionContext
{
	public class MyCallContext
	{
		public void Execute()
		{
			CallContext.SetData("Value", 123);
			
			var x = CallContext.GetData("Value");
			Console.WriteLine($"CallContext FirstGetData x : {x}\nManagedThreadId : {Thread.CurrentThread.ManagedThreadId}");

			// CallContextの場合別スレッド内で書き込んだ情報も保持される
			Task.Run(() =>
			{
				CallContext.SetData("Value", 1234);
				x = CallContext.GetData("Value");
				Console.WriteLine($"CallContext in Task.Run x : {x}\nManagedThreadId : {Thread.CurrentThread.ManagedThreadId}");

			}).Wait();
			
			// スレッド外
			Console.WriteLine($"CallContext after Task.Run() x : {x}\nManagedThreadId : {Thread.CurrentThread.ManagedThreadId}");
		}
	}

	/// <summary>
	/// CallContextは
	/// netstandard, netcoreでは使えないため自前で用意する
	/// </summary>
	public static class CallContext
	{
		private static ConcurrentDictionary<string, AsyncLocal<object>> state = new();

		public static void SetData(string name, object data) =>
			state
				.AddOrUpdate(name, new AsyncLocal<object>(), (_, _) => new AsyncLocal<object>())
				.Value = data;

		public static object GetData(string name) =>
			state.TryGetValue(name, out var data) ? data.Value : null;
	}
}