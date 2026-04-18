using UnityEngine;
using ZhengHua.Common;
namespace ZhengHua
{
    public class SettingManager : CommonCanvas
    {
        [SerializeField] private SliderHandle _mainVolumeHandle;
        [SerializeField] private SliderHandle _musicVolumeHandle;
        [SerializeField] private SliderHandle _sfxVolumeHandle;
        
        
        public void Apply()
        {
            UpdateSoundSetting();
            HidePanel();
        }

        public void Cancel()
        {
            HidePanel();
        }

        private void UpdateSoundSetting()
        {
            
        }
    }
}
