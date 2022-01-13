using System;
using System.Collections.Generic;
using System.Linq;

namespace XvoleR.Unity.EventBusModule
{
    public static class EventBusFactory
    {
        public enum EventBusType
        {
            InMemory
        }
        private static IEventBus _default;
        /// 
        ///The default event bus instance is recommended
        /// 
        public static IEventBus Default
        {
            get
            {
                if (_default == null)
                {
                    _default = Create(EventBusType.InMemory);
                }
                return _default;
            }
        }
        public static IEventBus Create(EventBusType eventBusType)
        {
            return new EventBusInMemory();
        }
        public static ICollection<Type> GetAllEventsTypes()
        {
            var type = typeof(IEventBase);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
                ;
            List<Type> events = new List<Type>();
            foreach (var item in types)
            {
                events.Add(item);
            }
            return events;
        }
        public static T Instantinate<T>() where T : IEventBase, new()
        {
            Type type = typeof(T);
            return (T)Instantinate(type);
        }
        public static IEventBase Instantinate(Type type)
        {
            IEventBase eventBase = (IEventBase)type.Assembly.CreateInstance(type.FullName);
            return eventBase;
        }
    }
}
