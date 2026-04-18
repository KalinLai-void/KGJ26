using UnityEngine;
namespace ZhengHua.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
    public class LevelData : ScriptableObject
    {
        public LevelDataItem[] levelDataItems;

        [System.Serializable]
        public class LevelDataItem 
        {
            public float startTime;
            public int shootCount;
            public float everyDelay;
            [SerializeField, Range(0f, 180f)]private float startAngle = 30f;
            [SerializeField, Range(0f, 180f)]private float endAngle = 150f;
            public float progress => Random.Range(startAngle, endAngle) / 180f;
        }
    }
}