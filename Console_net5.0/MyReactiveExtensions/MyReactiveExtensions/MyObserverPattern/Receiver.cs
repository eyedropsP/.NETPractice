using System;

namespace MyReactiveExtensions
{
	public class Receiver : IObserver<News>
	{
		/// <summary>
		/// 受信者名
		/// </summary>
		public string Name { get; private set; }

		public Receiver(string name)
		{
			Name = name;
		}
		
		/// <summary>
		/// プッシュベースの通知の送信を完了したことをオブザーバーに通知
		/// </summary>
		public void OnCompleted()
		{
			Console.WriteLine($"{Name} : OnCompleted");
		}

		/// <summary>
		/// エラーが発生したことをオブザーバーに通知
		/// </summary>
		/// <param name="error"></param>
		public void OnError(Exception error)
		{
			Console.WriteLine($"{Name} : OnError");
		}

		/// <summary>
		/// オブザーバーに新しいデータを提供
		/// </summary>
		/// <param name="value"></param>
		public void OnNext(News value)
		{
			Console.WriteLine($"{Name} : OnNext [{value.Title} - {value.Content}]");
		}
	}
}