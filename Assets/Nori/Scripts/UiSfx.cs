using UnityEngine;

namespace Nori
{
    /// <summary>UI 按鈕等：拖入 <see cref="AudioLibrary"/>，將方法綁到 Button OnClick。</summary>
    public class UiSfx : MonoBehaviour
    {
        [SerializeField] private AudioLibrary _audioLibrary;

        public void PlayButtonClick()
        {
            Play(SfxId.Button);
        }

    public void PlayStationeryHover()
    {
    Play(SfxId.StationeryHover);
    }

    public void PlayStationeryRotate()
    {
    Play(SfxId.StationeryRotate);
    }
        public void Play(SfxId id, float volumeScale = 1f)
        {
            if (_audioLibrary == null)
                return;

            _audioLibrary.PlaySfx(id, volumeScale);
        }
    }
}
