using UnityEngine;
using UnityEngine.Events;
using ZhengHua.Common;

namespace ZhengHua
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private UnityEvent onStage1StartEvent = new();
        [SerializeField] private UnityEvent onStage1FinishEvent = new();
        public static UnityEvent OnStage1Start() => Instance.onStage1StartEvent;
        public static UnityEvent OnStage1Finish() => Instance.onStage1FinishEvent; 
        
        [SerializeField] private UnityEvent onStage2StartEvent = new();
        [SerializeField] private UnityEvent onStage2FinishEvent = new();
        public static UnityEvent OnStage2Start() => Instance.onStage2StartEvent;
        public static UnityEvent OnStage2Finish() => Instance.onStage2FinishEvent; 
    }
}