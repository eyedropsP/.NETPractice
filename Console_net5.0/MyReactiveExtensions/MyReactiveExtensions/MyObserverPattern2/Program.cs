using System;

namespace MyReactiveExtensions2
{
	class Program
	{
		static void Main(string[] args)
		{
			var provider = new Provider<string>();
			provider.Subscribe(
				value => Console.WriteLine($"OnNext : {value}"),
				error => Console.WriteLine($"OnError : {error}"),
				() => Console.WriteLine($"OnCompleted")
			);
			provider.Notify("Test Message 1");
			provider.Notify(null);
			provider.Completed();
			provider.Notify("Test Message 2");
		}
	}
}