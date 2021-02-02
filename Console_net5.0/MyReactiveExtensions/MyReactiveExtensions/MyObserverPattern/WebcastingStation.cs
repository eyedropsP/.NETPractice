using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyReactiveExtensions
{
	public class WebcastingStation : IObservable<News>
	{
		/// <summary>
		/// 購読を解除する機能を提供
		/// </summary>
		private class UnSubscriber : IDisposable
		{
			/// <summary>
			/// オブザーバーのコレクションを保持
			/// </summary>
			private readonly LinkedList<IObserver<News>> observers = null;

			/// <summary>
			/// オブザーバーを保持
			/// </summary>
			private readonly IObserver<News> observer = null;

			public UnSubscriber(LinkedList<IObserver<News>> observers, IObserver<News> observer)
			{
				this.observers = observers;
				this.observer = observer;
			}
			
			/// <summary>
			/// リソース解放
			/// </summary>
			public void Dispose()
			{
				if (observers.Contains(observer))
				{
					observers.Remove(observer);
				}
			}
		}

		/// <summary>
		/// オブザーバーのコレクションを保持
		/// </summary>
		private readonly LinkedList<IObserver<News>> observers = new LinkedList<IObserver<News>>();

		/// <summary>
		/// オブザーバーが通知を受け取ることをプロバイダーに通知
		/// </summary>
		/// <param name="observer"></param>
		/// <returns></returns>
		public IDisposable Subscribe(IObserver<News> observer)
		{
			if (!observers.Contains(observer))
			{
				observers.AddLast(observer);
			}
			return new UnSubscriber(observers, observer);
		}

		public void Deliver(News news)
		{
			foreach (var observer in observers)
			{
				if (news == null)
				{
					observer.OnError(new ArgumentNullException());
				}
				else
				{
					observer.OnNext(news);
				}
			}
		}

		public void StopDelivering()
		{
			foreach (var observer in observers)
			{
				observer.OnCompleted();
			}
			observers.Clear();
		}
	}
}