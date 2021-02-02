using System;

namespace MyReactiveExtensions2
{
	/// <summary>
	/// 情報の受信者としての機能を提供
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Observer<T> : IObserver<T>
	{
		private readonly Action onCompleted = null;
		private readonly Action<Exception> onError = null;
		private readonly Action<T> onNext = null;

		public Observer(Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			this.onNext = onNext;
			this.onError = onError;
			this.onCompleted = onCompleted;
		}
		
		public void OnCompleted()
		{
			onCompleted?.Invoke();
		}

		public void OnError(Exception error)
		{
			onError?.Invoke(error);
		}

		public void OnNext(T value)
		{
			onNext?.Invoke(value);
		}
	}
}