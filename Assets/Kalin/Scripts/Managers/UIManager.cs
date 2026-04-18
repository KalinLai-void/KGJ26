using TMPro;
using UnityEngine;
using ZhengHua;

namespace KalinKonta
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private TextMeshProUGUI timeText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void Update()
        {
            if (GameManager.currentStage != GameManager.State.OnStage1Start) return;

            UpdateTimeText();
        }

        private void UpdateTimeText()
        {
            if (timeText == null) return;

            int mins = (int)GameManager.stage1Time / 60;
            int secs = (int)GameManager.stage1Time % 60;

            timeText.text = mins.ToString("00") + ":" + secs.ToString("00");
        }
    }
}
