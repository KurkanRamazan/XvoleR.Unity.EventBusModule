using UnityEngine;

namespace XvoleR.Unity.EventBusModule.Demo
{
    public class DemoManagerComponent : MonoBehaviour
    {
        public StateSingleton state = StateSingleton.Instance;
        private void Awake()
        {
            StateSingleton.Instance.Load();
            RegisterEvents();
        }
        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }


        private void RegisterEvents()
        {
            UnregisterEvents();

            ScoreChangerEvent.Subscribe(Event_ScoreChanger);
            ScoreRequestEvent.Subscribe(Event_ScoreRequested);
        }

        private void UnregisterEvents()
        {
            ScoreChangerEvent.Unsubscribe(Event_ScoreChanger);
            ScoreRequestEvent.Unsubscribe(Event_ScoreRequested);
        }

        private void Event_ScoreChanger(object sender, ScoreChangerEvent e)
        {
            StateSingleton.Instance.score += e.Difference;
            StateSingleton.Instance.Save();
            ScoreChangedEvent.FireEvent(this);
        }

        private void Event_ScoreRequested(object sender, ScoreRequestEvent e)
        {
            ScoreChangedEvent.FireEvent(this);
        }
    }
}
