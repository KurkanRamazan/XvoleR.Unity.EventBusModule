using UnityEngine;

namespace XvoleR.Unity.EventBusModule
{
    [CreateAssetMenu(fileName = "Events Generator", menuName = "XvoleR/EventBusModule/EventsGenerator")]
    public class EventsGenerator : ScriptableObject
    {
        [System.Serializable]
        public class Parameter
        {
            public string name;
            public string type;
        }
        [System.Serializable]
        public class EventNameDefinition
        {
            public string name;
            public bool singleton = true;
            public Parameter[] parameters;
            [Multiline]
            public string description;
        }
        [System.Serializable]
        public class EventsDefinition
        {
            public string name;
            public string _namespace;
            public string prefix;
            [Multiline]
            public string description;
            public EventNameDefinition[] eventNames;
        }
        public string[] usings;
        public EventsDefinition[] events;
    }
}
