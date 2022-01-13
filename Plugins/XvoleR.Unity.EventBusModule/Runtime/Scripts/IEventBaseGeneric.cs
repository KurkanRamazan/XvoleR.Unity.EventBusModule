using System;

namespace XvoleR.Unity.EventBusModule
{
    public interface IEventBase<T> : IEventBase where T : EventArgs
    {
        void Subscribe(EventHandler<T> eventHandler);
        void Unsubscribe(EventHandler<T> eventHandler);
        void Publish(object sender, T eventArgs);
    }
}
