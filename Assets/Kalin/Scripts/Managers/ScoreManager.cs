using KalinKonta.Stationery;
using UnityEngine;
using ZhengHua;

namespace KalinKonta
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        [SerializeField] private int poopScore = 5;

        private int score = 0;

        private void OnEnable()
        {
            GameManager.OnClickedPoop.AddListener(ClickPoopScore);
            GameManager.OnStage1Finish?.AddListener(TurnLeftCostToScore);
        }

        private void OnDisable()
        {
            GameManager.OnClickedPoop.RemoveListener(ClickPoopScore);
            GameManager.OnStage1Finish?.RemoveListener(TurnLeftCostToScore);
        }

        private void Awake()
        {
            if(Instance == null) Instance = this;
        }

        public void AddScore(int value)
        {
            score += value;
            UIManager.Instance.UpdateScoreText(score);
        }

        private void TurnLeftCostToScore()
        {
            AddScore(StationerySpawner.Instance.LeftCost);
        }

        private void ClickPoopScore()
        {
            StationerySpawner.Instance.TotalValidCost++;
            StationerySpawner.Instance.Init();
            AddScore(poopScore);
        }
    }
}
