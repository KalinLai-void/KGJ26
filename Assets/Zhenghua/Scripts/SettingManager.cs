using UnityEngine;

namespace ZhengHua
{
    public class SettingManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private SliderHandle _mainVolumeHandle;
        [SerializeField] private SliderHandle _musicVolumeHandle;
        [SerializeField] private SliderHandle _sfxVolumeHandle;
        
        
        public void Apply()
        {
            UpdateSoundSetting();
            HideSettingPanel();
        }

        public void Cancel()
        {
            HideSettingPanel();
        }

        private void UpdateSoundSetting()
        {
            
        }

        public void ShowSettingPanel()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public void HideSettingPanel()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}
