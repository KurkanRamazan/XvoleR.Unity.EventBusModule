using NUnit.Framework;
using System;

namespace XvoleR.Unity.EventBusModule.Tests
{
    public class EventBusTests
    {
        public class IntEventArgs : EventArgs
        {
            public int Value { get; set; }
        }
        public class StringEventArgs : EventArgs
        {
            public string Value { get; set; }
        }
        public class Test1Event : EventBaseContainer<IntEventArgs> { }
        public class Test2Event : EventBaseContainer<StringEventArgs> { }

        // A Test behaves as an ordinary method
        [Test(Description = "EventBus subscribe test")]
        public void EvntBus_Subscribe()
        {
            IEventBus eventBus = EventBusFactory.Create(EventBusFactory.EventBusType.InMemory);
            EventHandler<IntEventArgs> eventHandler = (sender, eventArgs) => { };
            eventBus.Subscribe(eventHandler);
            var subcription = eventBus.GetSubscription<IntEventArgs>();
            Assert.AreEqual(1, subcription.Count);
        }
        [Test(Description = "EventBus unsubscribe test")]
        public void EvntBus_Unsubscribe()
        {
            IEventBus eventBus = EventBusFactory.Create(EventBusFactory.EventBusType.InMemory);
            EventHandler<IntEventArgs> eventHandler = (sender, eventArgs) => { };
            eventBus.Subscribe(eventHandler);
            eventBus.Unsubscribe(eventHandler);
            var subcription = eventBus.GetSubscription<IntEventArgs>();
            Assert.AreEqual(0, subcription.Count);
        }
        // A Test behaves as an ordinary method
        [Test(Description = "EventBus subscribe&publish test")]
        public void EvntBus_SubscribeAndPublish()
        {
            IEventBus eventBus = EventBusFactory.Create(EventBusFactory.EventBusType.InMemory);
            int testValue = 0;
            EventHandler<IntEventArgs> eventHandler = (sender, eventArgs) =>
            {
                testValue = eventArgs.Value;
            };
            eventBus.Subscribe(eventHandler);
            eventBus.Publish(this, new IntEventArgs() { Value = 1 });
            // Use the Assert class to test conditions
            Assert.AreEqual(1, testValue);
        }

        // A Test behaves as an ordinary method
        [Test(Description = "EventBus subscribe sequence test")]
        public void EvntBus_Subscribe_Sequence()
        {
            IEventBus eventBus = EventBusFactory.Create(EventBusFactory.EventBusType.InMemory);
            int testValue = 1;
            EventHandler<IntEventArgs> eventHandlerFirst = (sender, eventArgs) =>
            {
                if (1 == testValue)
                {
                    testValue = eventArgs.Value;
                }
            };
            EventHandler<IntEventArgs> eventHandlerSecond = (sender, eventArgs) =>
            {
                if (2 == testValue)
                {
                    testValue = eventArgs.Value;
                }
            };
            eventBus.Subscribe(eventHandlerFirst);
            eventBus.Subscribe(eventHandlerSecond);

            eventBus.Publish(this, new IntEventArgs() { Value = 2 });
            eventBus.Publish(this, new IntEventArgs() { Value = 3 });
            // Use the Assert class to test conditions
            Assert.AreEqual(3, testValue);
        }

        // A Test behaves as an ordinary method
        [Test(Description = "EventBus multiple event test")]
        public void EvntBus_MultipleEventType()
        {
            IEventBus eventBus = EventBusFactory.Create(EventBusFactory.EventBusType.InMemory);
            int testValue = 1;
            string testValueString = string.Empty;
            EventHandler<IntEventArgs> eventHandlerFirst = (sender, eventArgs) =>
            {
                if (1 == testValue)
                {
                    testValue = eventArgs.Value;
                }
            };
            EventHandler<StringEventArgs> eventHandlerSecond = (sender, eventArgs) =>
            {
                if (2 == testValue)
                {
                    testValueString = eventArgs.Value;
                }
            };
            try
            {
                eventBus.Subscribe(eventHandlerFirst);
                eventBus.Subscribe(eventHandlerSecond);

                eventBus.Publish(this, new IntEventArgs() { Value = 2 });
                eventBus.Publish(this, new StringEventArgs() { Value = "Demo" });
            }
            finally
            {
                eventBus.Unsubscribe(eventHandlerFirst);
                eventBus.Unsubscribe(eventHandlerSecond);
            }
            // Use the Assert class to test conditions
            Assert.AreEqual(2, testValue);
            Assert.AreEqual("Demo", testValueString);
        }


        // A Test behaves as an ordinary method
        [Test(Description = "EventBus subscribe same handler test")]
        public void EvntBus_Subscribe_Same_Handler()
        {
            IEventBus eventBus = EventBusFactory.Create(EventBusFactory.EventBusType.InMemory);
            int testValue = 1;
            EventHandler<IntEventArgs> eventHandlerFirst = (sender, eventArgs) =>
            {
                testValue = eventArgs.Value * testValue;
            };
            eventBus.Subscribe(eventHandlerFirst);
            eventBus.Publish(this, new IntEventArgs() { Value = 2 });
            Assert.AreEqual(1 * 2, testValue);

            testValue = 1;
            eventBus.Subscribe(eventHandlerFirst);
            eventBus.Publish(this, new IntEventArgs() { Value = 2 });
            Assert.AreEqual(1 * 2 * 2, testValue);

            testValue = 1;
            eventBus.Unsubscribe(eventHandlerFirst);
            eventBus.Publish(this, new IntEventArgs() { Value = 2 });
            Assert.AreEqual(1 * 2, testValue);

            testValue = 1;
            eventBus.Unsubscribe(eventHandlerFirst);
            eventBus.Publish(this, new IntEventArgs() { Value = 2 });
            Assert.AreEqual(1, testValue);
        }
    }
}
