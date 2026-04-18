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
        }
    }
}