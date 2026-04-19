using System;
using Nori;
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
        [Header("Audio")]
        [SerializeField] private AudioLibrary _audioLibrary;
        private void Start()
        {
            print("GameEndManager Start");
            GameManager.OnGameEnd?.AddListener(OnGameEnd);
        }
        
        private void OnDestroy()
        {
            GameManager.OnGameEnd?.RemoveListener(OnGameEnd);
        }

        private void OnGameEnd(bool isWin)
        {
            _winPanel.SetActive(isWin);
            _losePanel.SetActive(!isWin);
            
            _audioLibrary.PlaySfx(isWin ? SfxId.GameWin : SfxId.GameFail);
            AudioManager.StopMusic();


            ShowPanel();
        }

        public void BackToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(MenuSceneName);
        }

        public void Retry()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public override void ShowPanel()
        {
            base.ShowPanel();

            Time.timeScale = 0f;
        }

        public override void HidePanel()
        {
            base.HidePanel();
            
            Time.timeScale = 1f;
        }
    }
}