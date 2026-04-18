using System;
using UnityEngine;
using UnityEngine.Events;
using ZhengHua.Common;
using Random = UnityEngine.Random;

namespace ZhengHua
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private UnityEvent onStage1StartEvent = new();
        [SerializeField] private UnityEvent onStage1FinishEvent = new();
        public static UnityEvent OnStage1Start => Instance?.onStage1StartEvent;
        public static UnityEvent OnStage1Finish => Instance?.onStage1FinishEvent; 
        
        [SerializeField] private UnityEvent onStage2StartEvent = new();
        [SerializeField] private UnityEvent<bool> onStage2FinishEvent = new();
        public static UnityEvent OnStage2Start => Instance?.onStage2StartEvent;
        public static UnityEvent<bool> OnStage2Finish => Instance?.onStage2FinishEvent;
        private bool _isWin = false;

        private void Start()
        {
            Invoke(nameof(EnterStage1), 0.1f);
        }

        [SerializeField] private float _stage1Time = 180f;

        private void EnterStage1()
        {
            print("EnterStage1");
            onStage1StartEvent?.Invoke();
            Invoke(nameof(Stage1TimeOut), _stage1Time);
        }

        private void Stage1TimeOut()
        {
            print("Stage1TimeOut");
            onStage1FinishEvent?.Invoke();
            Invoke(nameof(EnterStage2), 1f);
        }

        public void SkipStage1()
        {
            print("SkipStage1");
            CancelInvoke(nameof(Stage1TimeOut));
            Stage1TimeOut();
        }
        
        private void EnterStage2()
        {
            print("EnterStage2");
            onStage2StartEvent?.Invoke();
        }

        private void Stage2End()
        {
            print("Stage2End");

            _isWin = Random.value > 0.5f;
            onStage2FinishEvent?.Invoke(_isWin);
        }
    }
}