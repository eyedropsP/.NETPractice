using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace MyReactiveExtensions2
{
	/// <summary>
	/// 情報の発信源としての機能を提供する
	/// </summary>
	/// <typeparam name="T">発信するデータの型</typeparam>
	public class Provider<T> : IObservable<T>
	{
		private readonly LinkedList<IObserver<T>> observers = new LinkedList<IObserver<T>>();
		public IDisposable Subscribe(IObserver<T> observer)
		{
			if (!observers.Contains(observer))
			{
				observers.AddLast(observer);
			}

			return null;
		}

		public void Notify(T value)
		{
			if (value == null || value.Equals(default(T)))
			{
				foreach (var observer in observers)
				{
					observer.OnError(new ArgumentException());
				}
			}
			else
			{
				foreach (var observer in observers)
				{
					observer.OnNext(value);
				}
			}
		}

		public void Completed()
		{
			foreach (var observer in observers)
			{
				observer.OnCompleted();
			}
			observers.Clear();
		}
	}
}