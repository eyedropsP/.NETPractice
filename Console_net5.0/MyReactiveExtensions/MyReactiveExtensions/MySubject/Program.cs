using System;

namespace MySubject
{
	static class Program
	{
		static void Main(string[] args)
		{
			var mySubject = new MySubject<int>();

			var myObserver1 = new MyObserver("1");
			var myObserver2 = new MyObserver("2");
			var myObserver3 = new MyObserver("3");

			var disposable1 = mySubject.Subscribe(myObserver1);
			var disposable2 = mySubject.Subscribe(myObserver2);
			var disposable3 = mySubject.Subscribe(myObserver3);
			
			mySubject.OnNext(100);
			
			disposable1.Dispose();
			mySubject.OnNext(111);

			disposable2.Dispose();
			
			mySubject.OnError(new ArgumentException("error!!!"));
			mySubject.OnNext(222);
		}

		public class MyObserver : IObserver<int>
		{
			private string name;

			public MyObserver(string name)
			{
				this.name = name;
			}
			
			public void OnCompleted()
			{
				Console.WriteLine($"{name}'s OnCompleted");
			}

			public void OnError(Exception error)
			{
				Console.WriteLine($"{name}'s OnError : {error.Message}");
			}

			public void OnNext(int value)
			{
				Console.WriteLine($"{name}'s OnNext : {value}");
			}
		}
	}
}