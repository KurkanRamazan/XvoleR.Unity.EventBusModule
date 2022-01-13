using System;
using System.Collections.Generic;

namespace XvoleR.Unity.EventBusModule
{
    public class EventBaseContainer<T> : IEventBase<T> where T : EventArgs
    {
        protected readonly List<EventHandler<T>> subscriptions = new List<EventHandler<T>>();
        public int Count { get { return subscriptions.Count; } }
        public void Subscribe(EventHandler<T> eventHandler)
        {
            subscriptions.Add(eventHandler);
        }

        public void Unsubscribe(EventHandler<T> eventHandler)
        {
            subscriptions.Remove(eventHandler);
        }

        public virtual void Publish(object sender, T eventArgs)
        {
            for (int i = 0; i < subscriptions.Count; i++)
            {
                subscriptions[i](sender, eventArgs);
            }
        }
        public void Clear()
        {
            subscriptions.Clear();
        }
    }
}
