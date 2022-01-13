using UnityEngine;

namespace XvoleR.Unity.EventBusModule.Demo
{
    [System.Serializable]
    public class StateSingleton
    {
        public static readonly StateSingleton Instance = new StateSingleton();
        public int score;
        private StateSingleton()
        {
        }

        public void Save()
        {
            PlayerPrefs.SetInt("XvoleR.EventBusModule.Demo.StateSingleton:score", score);
        }

        public void Load()
        {
            score = PlayerPrefs.GetInt("XvoleR.EventBusModule.Demo.StateSingleton:score");
        }
    }
}
