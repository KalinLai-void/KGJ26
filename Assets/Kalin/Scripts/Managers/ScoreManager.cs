using KalinKonta.Stationery;
using UnityEngine;
using ZhengHua;

namespace KalinKonta
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        [SerializeField] private int poopScore = 5;
        [SerializeField] private int leftCostScorePerOne = 50;

        private int score = 0;

        private void OnEnable()
        {
            GameManager.OnStage2Start?.AddListener(ResetCost);
            GameManager.OnClickedPoop?.AddListener(ClickPoopScore);
            GameManager.OnStage1Finish?.AddListener(TurnLeftCostToScore);
        }

        private void OnDisable()
        {
            GameManager.OnStage2Start?.RemoveListener(ResetCost);
            GameManager.OnClickedPoop?.RemoveListener(ClickPoopScore);
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
            AddScore(CostManager.Instance.LeftCost * leftCostScorePerOne);
        }

        private void ClickPoopScore()
        {
            CostManager.Instance.TotalValidCost++;
            ResetCost();
            AddScore(poopScore);
        }

        private void ResetCost()
        {
            CostManager.Instance.ResetCost();
            UIManager.Instance.UpdateCostText(CostManager.Instance.TotalValidCost, 2);
        }
    }
}
