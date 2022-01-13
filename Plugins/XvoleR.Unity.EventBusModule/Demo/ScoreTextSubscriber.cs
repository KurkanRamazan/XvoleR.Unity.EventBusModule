using UnityEngine;
using UnityEngine.UI;

namespace XvoleR.Unity.EventBusModule.Demo
{
    public class ScoreTextSubscriber : MonoBehaviour
    {
        public Text text;
        public string format = "Score: {0}";
        private void Start()
        {
            ScoreRequestEvent.FireEvent(this);
        }
        private void OnEnable()
        {
            OnDisable();
            ScoreChangedEvent.Subscribe(Event_ScoreChanged);
        }
        private void OnDisable()
        {
            ScoreChangedEvent.Unsubscribe(Event_ScoreChanged);
        }

        private void Event_ScoreChanged(object sender, ScoreChangedEvent e)
        {
            var score = StateSingleton.Instance.score;
            RefreshUi(score);
        }
        private void RefreshUi(int score)
        {
            text.text = string.Format(format, score);
        }
    }
}
