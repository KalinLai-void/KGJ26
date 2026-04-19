using System;
using UnityEngine;
using UnityEngine.Events;
using ZhengHua.Common;
using Random = UnityEngine.Random;

namespace ZhengHua
{

    public class GameManager : Singleton<GameManager>
    {
        public enum State { OnStage1Start, OnStage1Finish, OnStage2Start, OnStage2Finish }

        [SerializeField] private UnityEvent onStage1StartEvent = new();
        [SerializeField] private UnityEvent onStage1FinishEvent = new();
        public static UnityEvent OnStage1Start => Instance?.onStage1StartEvent;
        public static UnityEvent OnStage1Finish => Instance?.onStage1FinishEvent; 
        
        [SerializeField] private UnityEvent onStage2StartEvent = new();
        [SerializeField] private UnityEvent<bool> onStage2FinishEvent = new();
        public static UnityEvent OnStage2Start => Instance?.onStage2StartEvent;
        public static UnityEvent<bool> OnStage2Finish => Instance?.onStage2FinishEvent;
        
        [SerializeField] private UnityEvent onClickedPoopEvent = new();
        public static UnityEvent OnClickedPoop => Instance?.onClickedPoopEvent;
        
        private bool _isWin = false;

        public static State currentStage;

        public float stage1TotalTime = 180f;
        public static float stage1Time;

        private void Start()
        {
            stage1Time = stage1TotalTime;
            Invoke(nameof(EnterStage1), 0.1f);
        }              

        private void Update()
        {
            if (stage1Time <= 0)
            {
                if (currentStage == State.OnStage1Start) Stage1TimeOut();
                stage1Time = 0;
                return;
            }
            stage1Time -= Time.deltaTime;
        }

        private void EnterStage1()
        {
            currentStage = State.OnStage1Start;
            print("EnterStage1");
            onStage1StartEvent?.Invoke();
            //Invoke(nameof(Stage1TimeOut), stage1Time);
        }

        public void Stage1TimeOut()
        {
            currentStage = State.OnStage1Finish;
            print("Stage1TimeOut");
            onStage1FinishEvent?.Invoke();
            Invoke(nameof(EnterStage2), 0f);
        }
        

        public void SkipStage1()
        {
            print("SkipStage1");
            //CancelInvoke(nameof(Stage1TimeOut));
            stage1Time = 0;
            Invoke(nameof(Stage1TimeOut), 0f);
        }
        
        private void EnterStage2()
        {
            currentStage = State.OnStage2Start;
            print("EnterStage2");
            onStage2StartEvent?.Invoke();
        }

        private void Stage2End()
        {
            currentStage = State.OnStage2Finish;
            print("Stage2End");

            _isWin = Random.value > 0.5f;
            onStage2FinishEvent?.Invoke(_isWin);
        }
    }
}