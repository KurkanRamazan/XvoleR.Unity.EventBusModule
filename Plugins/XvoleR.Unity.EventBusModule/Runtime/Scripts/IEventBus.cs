using System;
using System.Collections.Generic;

namespace XvoleR.Unity.EventBusModule
{
    public interface IEventBus
    {
        IEventBase GetEvent(Type type);
        TEventBase GetEvent<TEventBase>() where TEventBase : IEventBase, new();
        IEventBase RemoveEvent(Type type);
        TEventBase RemoveEvent<TEventBase>() where TEventBase : IEventBase, new();
        IReadOnlyCollection<Type> Events { get; }
        bool ContainsEvent(Type type);
        bool ContainsEvent<TEventBase>() where TEventBase : IEventBase, new();
        IEventBase this[Type type] { get; }
        IEventBase<TEventArgs> GetSubscription<TEventArgs>() where TEventArgs : EventArgs;
        void Subscribe<TEventArgs>(EventHandler<TEventArgs> eventHandler) where TEventArgs : EventArgs;
        void Unsubscribe<TEventArgs>(EventHandler<TEventArgs> eventHandler) where TEventArgs : EventArgs;
        void Publish<TEventArgs>(object sender, TEventArgs eventArgs) where TEventArgs : EventArgs;
    }
}
