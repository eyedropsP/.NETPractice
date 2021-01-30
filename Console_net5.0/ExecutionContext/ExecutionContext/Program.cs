using System;

namespace ExecutionContext
{
	internal static class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("MyStaticField\n");
			var myStaticField = new MyStaticField();
			myStaticField.Execute();

			Console.WriteLine("\nMyThreadLocal\n");
			var myThreadLocal = new MyThreadLocal();
			myThreadLocal.Execute();
			
			Console.WriteLine("\nMyCallContext\n");
			var myCallContext = new MyCallContext();
			myCallContext.Execute();
		}
	}
}