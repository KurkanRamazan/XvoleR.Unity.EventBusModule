using System;
using System.Collections.Generic;
using System.Linq;

namespace XvoleR.Unity.EventBusModule
{
    /// 
    ///Event bus
    /// 
    public class EventBusInMemory : IEventBus
    {
        private Dictionary<Type, IEventBase> eventDic = new Dictionary<Type, IEventBase>();


        /// 
        ///Constructor, automatically load the derived class implementation of eventbase
        /// 
        internal EventBusInMemory()
        {
        }

        /// 
        ///Get event instance
        /// 
        ///Event type
        /// 
        public TEvent GetEvent<TEvent>() where TEvent : IEventBase, new()
        {
            return (TEvent)eventDic[typeof(TEvent)];
        }

        /// 
        ///Add event type
        /// 
        /// 
        public TEvent AddEvent<TEvent>() where TEvent : IEventBase, new()
        {
            Type type = typeof(TEvent);
            if (!eventDic.TryGetValue(type, out var @event))
            {
                @event = new TEvent();
                eventDic.Add(type, @event);
            }
            return (TEvent)@event;
        }

        /// 
        ///Remove event type
        /// 
        /// 
        public TEventBase RemoveEvent<TEventBase>() where TEventBase : IEventBase, new()
        {
            return (TEventBase)RemoveEvent(typeof(TEventBase));
        }

        public void Subscribe<TEventArgs>(EventHandler<TEventArgs> eventHandler)
            where TEventArgs : EventArgs
        {
            var @event = AddEvent<EventBaseContainer<TEventArgs>>();
            @event.Subscribe(eventHandler);
        }
        public void Unsubscribe<TEventArgs>(EventHandler<TEventArgs> eventHandler)
            where TEventArgs : EventArgs
        {
            var @event = AddEvent<EventBaseContainer<TEventArgs>>();
            @event.Unsubscribe(eventHandler);
        }
        public void Publish<TEventArgs>(object sender, TEventArgs eventArgs)
            where TEventArgs : EventArgs
        {
            var @event = AddEvent<EventBaseContainer<TEventArgs>>();
            @event.Publish(sender, eventArgs);
        }
        public void Clear()
        {
            eventDic.Clear();
        }

        public IEventBase GetEvent(Type type)
        {
            return eventDic[type];
        }

        public IEventBase RemoveEvent(Type type)
        {
            if (eventDic.TryGetValue(type, out var t))
            {
                eventDic.Remove(type);
            }
            return t;
        }

        public bool ContainsEvent(Type type)
        {
            return eventDic.ContainsKey(type);
        }

        public bool ContainsEvent<TEventBase>() where TEventBase : IEventBase, new()
        {
            return ContainsEvent(typeof(TEventBase));
        }

        public IEventBase<TEventArgs> GetSubscription<TEventArgs>() where TEventArgs : EventArgs
        {
            return GetEvent<EventBaseContainer<TEventArgs>>();
        }

        public IReadOnlyCollection<Type> Events
        {
            get
            {
                return eventDic.Keys.ToList().AsReadOnly();
            }
        }

        public IEventBase this[Type type] => eventDic[type];
    }
}
