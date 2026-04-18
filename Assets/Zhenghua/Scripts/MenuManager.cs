using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZhengHua
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private string GameSceneName = "GameScene";
        public void GameStart()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}