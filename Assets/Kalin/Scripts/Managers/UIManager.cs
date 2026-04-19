using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZhengHua;

namespace KalinKonta
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private Button skipBtn;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void OnEnable()
        {
            GameManager.OnStage1Start?.AddListener(ShowSkipBtn);
            GameManager.OnStage2Start?.AddListener(HideSkipBtn);
        }

        private void OnDisable()
        {
            GameManager.OnStage1Start?.RemoveListener(ShowSkipBtn);
            GameManager.OnStage2Start?.RemoveListener(HideSkipBtn);
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void Update()
        {
            if (GameManager.currentStage != GameManager.State.OnStage1Start) return;

            UpdateTimeText();
        }

        private void HideSkipBtn()
        {
            skipBtn.gameObject.SetActive(false);
        }

        private void ShowSkipBtn()
        {
            skipBtn.gameObject.SetActive(true);
        }

        public void UpdateScoreText(int value)
        {
            if (!scoreText) return;

            scoreText.text = value.ToString("00000");
        }

        public void UpdateCostText(int value)
        {
            if (!costText || value < 0) return;

            costText.text = $"Left Costs:\r\n{value}";
        }

        private void UpdateTimeText()
        {
            if (!timeText) return;

            int mins = (int)GameManager.stage1Time / 60;
            int secs = (int)GameManager.stage1Time % 60;

            timeText.text = mins.ToString("00") + ":" + secs.ToString("00");
        }
    }
}
