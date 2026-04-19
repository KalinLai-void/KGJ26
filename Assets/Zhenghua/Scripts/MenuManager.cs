using System;
using Nori;
using UnityEngine;
using System.Collections;
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

        public void GameStartForDelay(float delay)
        {
            StartCoroutine(Delay(delay));
        }

        IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(GameSceneName);
        }

        public void Start()
        {
            AudioManager.PlayMusic(bgm);
            AudioManager.SetMusicVolume(0.6f);
        }
    }
}