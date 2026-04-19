using UnityEngine;

namespace KalinKonta
{
    public class CostManager : MonoBehaviour
    {
        public static CostManager Instance;

        [SerializeField] private int totalValidCost = 10;
        private int leftCost;

        public int LeftCost
        {
            get => leftCost; set => leftCost = value;
        }

        public int TotalValidCost
        {
            get => totalValidCost; set => totalValidCost = value;
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void ResetCost()
        {
            leftCost = totalValidCost;
        }

        public void AddCost(int value)
        {
            leftCost += value;
            if (leftCost <= 0) leftCost = 0;
        }
    }
}