using KalinKonta.Stationery;
using UnityEngine;
using ZhengHua;

namespace KalinKonta
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        private int score = 0;

        private void OnEnable()
        {
            GameManager.OnStage1Finish?.AddListener(TurnLeftCostToScore);
        }

        private void OnDisable()
        {
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
    }
}
