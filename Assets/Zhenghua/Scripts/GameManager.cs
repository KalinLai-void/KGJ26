using System;
using Nori;
using UnityEngine;
using UnityEngine.Events;
using ZhengHua.Common;
using ZhengHua.ScriptableObjects;
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
        [SerializeField] private UnityEvent onStage2FinishEvent = new();
        public static UnityEvent OnStage2Start => Instance?.onStage2StartEvent;
        public static UnityEvent OnStage2Finish => Instance?.onStage2FinishEvent;
        
        [SerializeField] private UnityEvent onClickedPoopEvent = new();
        public static UnityEvent OnClickedPoop => Instance?.onClickedPoopEvent;
        
        [SerializeField] private UnityEvent<bool> onGameEndEvent = new();
        public static UnityEvent<bool> OnGameEnd => Instance?.onGameEndEvent;

        public static State currentStage;

        [SerializeField]private float stage1TotalTime = 180f;
        private float stage1Timer = 0f;
        public static float stage1Time => Instance.stage1Timer;

        [SerializeField] private LevelData[] gameLevel;
        private int _levelIndex = 0;
        public static LevelData CurrentLevel => Instance.gameLevel[Instance._levelIndex];

        [SerializeField] private AudioClip bgm;
        [SerializeField] private AudioLibrary _audioLibrary;

        private void Start()
        {
            stage1Timer = stage1TotalTime;
            Invoke(nameof(EnterStage1), 0.1f);
            
            onStage2FinishEvent.AddListener(Stage2End);
            
            AudioManager.PlayMusic(bgm);
        }              

        private void Update()
        {
            if (currentStage == State.OnStage1Start)
            {
                if (stage1Time <= 0)
                {
                    if (currentStage == State.OnStage1Start) 
                        Stage1TimeOut();
                    stage1Timer = 0;
                    return;
                }

                stage1Timer -= Time.deltaTime;
            }
        }

        private void EnterStage1()
        {
            _audioLibrary.PlaySfx(SfxId.GameStart);
            currentStage = State.OnStage1Start;
            stage1Timer = stage1TotalTime;
            isPerformed = false;
            print("EnterStage1");
            onStage1StartEvent?.Invoke();
        }

        bool isPerformed = false; // Avoid Repeats

        public void Stage1TimeOut()
        {
            if (isPerformed) return;
            isPerformed = true;

            currentStage = State.OnStage1Finish;
            print("Stage1TimeOut");
            onStage1FinishEvent?.Invoke();
            Invoke(nameof(EnterStage2), 0f);
        }
        

        public void SkipStage1()
        {
            print("SkipStage1");
            stage1Timer = 0;
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
            
            _levelIndex++;
            if(_levelIndex > gameLevel.Length - 1)
            {
                onGameEndEvent?.Invoke(true);
            }
            else
            {
                EnterStage1();
            }
        }
    }
}