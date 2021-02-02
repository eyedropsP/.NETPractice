using System;

namespace MyReactiveExtensions2
{
	public static class ObservableHelper
	{
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError,
			Action onCompleted)
		{
			return source.Subscribe(new Observer<T>(onNext, onError, onCompleted));
		}
	}
}