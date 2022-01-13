using UnityEngine;
using UnityEngine.UI;

namespace XvoleR.Unity.EventBusModule.Demo
{
    public class ExpBarComponent : MonoBehaviour
    {
        public Text textCurrentLevel;
        public Image imageExpForeground;
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
            int i = 0;
            while (i < scores.Length && score >= scores[i])
            {
                i++;
            }
            i = Mathf.Min(i, scores.Length - 1);
            score = Mathf.Min(score, scores[scores.Length - 1]);

            int previousScore = i > 0 ? scores[i - 1] : 0;

            var rate = (float)(score - previousScore) / (scores[i] - previousScore);
            imageExpForeground.fillAmount = rate;
            textCurrentLevel.text = string.Format("Level: {0} / {1}", i + 1 + (score == scores[scores.Length - 1] ? 1 : 0), scores.Length + 1);
        }
    }
}