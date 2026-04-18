using UnityEngine;

namespace ZhengHua.Common
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class CommonCanvas : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        
        public virtual void ShowPanel()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public virtual void HidePanel()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}