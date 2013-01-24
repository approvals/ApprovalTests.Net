
using System;
using System.Reflection;
using System.Windows;
using Microsoft.Silverlight.Testing;

namespace ApprovalTests.Silverlight.Tests
{
	public static class TestHelper
	{
		//TODO: need some way of timing out
		public static void WaitFor<T>(WorkItemTest test, T objectToWaitForItsEvent, string eventName)
		{
			EventInfo eventInfo = objectToWaitForItsEvent.GetType().GetEvent(eventName);
			
			bool eventRaised = false;

			if (typeof(RoutedEventHandler).IsAssignableFrom(eventInfo.EventHandlerType))
				eventInfo.AddEventHandler(objectToWaitForItsEvent, (RoutedEventHandler)delegate { eventRaised = true; });
			else if (typeof(EventHandler).IsAssignableFrom(eventInfo.EventHandlerType))
				eventInfo.AddEventHandler(objectToWaitForItsEvent, (EventHandler)delegate { eventRaised = true; });

			test.EnqueueConditional(() => eventRaised);
		}
	}
}
