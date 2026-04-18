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
            if (GameManager.Instance.currentStage != GameManager.State.OnStage1Start) return;

            GameManager.Instance.stage1Time -= Time.deltaTime;
            UpdateTimeText();
            if (GameManager.Instance.stage1Time > 0) return;

            GameManager.Instance.SkipStage1();
        }

        private void UpdateTimeText()
        {
            if (timeText == null) return;

            int mins = (int)GameManager.Instance.stage1Time / 60;
            int secs = (int)GameManager.Instance.stage1Time % 60;

            timeText.text = mins.ToString("00") + ":" + secs.ToString("00");
        }
    }
}
