namespace XvoleR.Unity.EventBusModule
{
    public interface IEventBase
    {
        /// <summary>
        /// Count of subscribers
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Clear subscriptions
        /// </summary>
        void Clear();
    }
}
