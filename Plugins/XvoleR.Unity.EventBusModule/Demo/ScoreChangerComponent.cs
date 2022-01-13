using UnityEngine;

namespace XvoleR.Unity.EventBusModule.Demo
{
    public class ScoreChangerComponent : MonoBehaviour
    {
        public void Event_UI_Click_UpdateScore(int diff)
        {
            ScoreChangerEvent.FireEvent(this, diff);
        }
    }
}
