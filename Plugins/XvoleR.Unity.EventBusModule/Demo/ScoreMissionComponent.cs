using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace XvoleR.Unity.EventBusModule.Demo
{
    public class ScoreMissionComponent : MonoBehaviour
    {
        public Text text;
        public int[] scores;
        private void Start()
        {
            ScoreRequestEvent.FireEvent(this);
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
            ScoreChangedEvent.Subscribe(Event_ScoreChanged);
        }
        private void UnregisterEvents()
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
            var msg = new StringBuilder();
            int i;
            for (i = 0; i < scores.Length && scores[i] <= score; i++)
            {
                msg.AppendFormat("<color=green>{0}</color>", scores[i]);
                msg.AppendLine();
            }
            for (; i < scores.Length; i++)
            {
                msg.AppendFormat("<color=red>{0}</color>", scores[i]);
                msg.AppendLine();
            }
            text.text = msg.ToString();
        }
    }
}
