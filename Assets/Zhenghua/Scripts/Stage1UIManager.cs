using System;
using UnityEngine;
using ZhengHua.Common;

namespace ZhengHua
{
    public class Stage1UIManager : CommonCanvas
    {

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameManager.OnStage1Start?.AddListener(ShowPanel);
            GameManager.OnStage1Finish?.AddListener(HidePanel);
        }
        
        private void OnDestroy()
        {
            GameManager.OnStage1Start?.RemoveListener(ShowPanel);
            GameManager.OnStage1Finish?.RemoveListener(HidePanel);
        }
    }
}