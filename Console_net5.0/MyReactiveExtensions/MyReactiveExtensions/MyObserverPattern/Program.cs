using System;

namespace MyReactiveExtensions
{
	class Program
	{
		static void Main(string[] args)
		{
			var provider = new WebcastingStation();
			var unSubscriber1 = provider.Subscribe(new Receiver("Jack"));
			var unSubscriber2 = provider.Subscribe(new Receiver("Nick"));
			var unSubscriver3 = provider.Subscribe(new Receiver("Mick"));
			
			provider.Deliver(new News("Title1", "Content1"));
			unSubscriber1.Dispose();
			provider.Deliver(null);
			provider.Deliver(new News("Title2", "Content2"));
			unSubscriber2.Dispose();
			provider.StopDelivering();
			provider.Deliver(new News("Title3", "Content3"));
			unSubscriver3.Dispose();
		}
	}
}