using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZhengHua.Common;

namespace ZhengHua
{
    public class GameEndManager : CommonCanvas
    {
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private string MenuSceneName = "Menu";
        private void Start()
        {
            print("GameEndManager Start");
            GameManager.OnStage2Finish?.AddListener(OnGameEnd);
        }
        
        private void OnDestroy()
        {
            GameManager.OnStage2Finish?.RemoveListener(OnGameEnd);
        }

        private void OnGameEnd(bool isWin)
        {
            print("OnGameEnd " + isWin);
            _winPanel.SetActive(isWin);
            _losePanel.SetActive(!isWin);
            
            ShowPanel();
        }
        
        public void BackToMenu() => SceneManager.LoadScene(MenuSceneName);
        public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}