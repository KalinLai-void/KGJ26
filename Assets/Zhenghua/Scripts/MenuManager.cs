using System;
using Nori;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZhengHua
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private string GameSceneName = "GameScene";
        [SerializeField] private AudioClip bgm;
        public void GameStart()
        {
            SceneManager.LoadScene(GameSceneName);
        }

        public void Start()
        {
            AudioManager.PlayMusic(bgm);
            AudioManager.SetMusicVolume(0.6f);
        }
    }
}