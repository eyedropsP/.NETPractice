using System;
using System.Collections.Generic;

namespace MySubject
{
	/// <summary>
	/// 自作のSubject
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MySubject<T> : IObservable<T>, IObserver<T>
	{
		/// <summary>
		/// 購読解除する機能の提供
		/// </summary>
		/// <typeparam name="U"></typeparam>
		private class UnSubscriber<U> : IDisposable
		{
			private LinkedList<IObserver<U>> observers = null;
			private readonly IObserver<U> observer = null;

			public UnSubscriber(LinkedList<IObserver<U>> observers, IObserver<U> observer)
			{
				this.observers = observers;
				this.observer = observer;
			}
			
			public void Dispose()
			{
				if (observers.Contains(observer))
				{
					observers.Remove(observer);
				}
			}
		}

		/// <summary>
		/// 完了したかどうかを保持
		/// </summary>
		private bool isCompleted = false;

		/// <summary>
		/// 発生した例外を保持
		/// </summary>
		private Exception error = null;

		private readonly LinkedList<IObserver<T>> observers = new LinkedList<IObserver<T>>();

		public IDisposable Subscribe(IObserver<T> observer)
		{
			if (!isCompleted)
			{
				if (!observers.Contains(observer))
				{
					observers.AddLast(observer);
				}
			}
			else if (error == null)
			{
				observer.OnCompleted();
			}
			else
			{
				observer.OnError(error);
			}

			return new UnSubscriber<T>(observers, observer);
		}

		public void OnCompleted()
		{
			if (isCompleted)
				return;

			foreach (var observer in observers)
			{
				observer.OnCompleted();
			}
			observers.Clear();
			isCompleted = true;
		}
		
		public void OnError(Exception error)
		{
			if (isCompleted)
				return;

			foreach (var observer in observers)
			{
				observer.OnError(error);
			}
			observers.Clear();
			this.error = error;
			isCompleted = true;
		}

		public void OnNext(T value)
		{
			if (isCompleted) return;
			foreach (var observer in observers)
			{
				observer.OnNext(value);
			}
		}
	}
}