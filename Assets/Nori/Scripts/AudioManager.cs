using UnityEngine;
using ZhengHua.Common;

namespace Nori
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource _sfx;
        [SerializeField] private AudioSource _music;

        protected override void Awake()
        {
            base.Awake();
            if (Instance != this)
                return;

            EnsureSources();
            DontDestroyOnLoad(gameObject);
        }

        private void EnsureSources()
        {
            if (_sfx == null)
            {
                _sfx = gameObject.AddComponent<AudioSource>();
                _sfx.loop = false;
            }

            _sfx.spatialBlend = 0f;

            if (_music == null)
            {
                _music = gameObject.AddComponent<AudioSource>();
                _music.loop = true;
            }

            _music.spatialBlend = 0f;
            _music.loop = true;
        }

        /// <summary>短音效；volumeScale 為額外乘在 clip 上的係數（仍會乘上 SFX AudioSource 的 volume）。</summary>
        public static void PlaySFX(AudioClip clip, float volumeScale = 1f)
        {
            if (clip == null || Instance == null)
                return;

            Instance._sfx.PlayOneShot(clip, volumeScale);
        }

        /// <summary>背景音樂；若已在播放同一個 clip 則不重新從頭播。</summary>
        public static void PlayMusic(AudioClip clip)
        {
            if (clip == null || Instance == null)
                return;

            Instance.PlayMusicInternal(clip);
        }

        private void PlayMusicInternal(AudioClip clip)
        {
            if (_music.clip == clip && _music.isPlaying)
                return;

            _music.clip = clip;
            _music.loop = true;
            _music.Play();
        }

        public static void StopMusic()
        {
            if (Instance == null)
                return;

            Instance._music.Stop();
        }

        public static void SetSfxVolume(float volume01)
        {
            if (Instance == null)
                return;

            Instance._sfx.volume = Mathf.Clamp01(volume01);
        }

        public static void SetMusicVolume(float volume01)
        {
            if (Instance == null)
                return;

            Instance._music.volume = Mathf.Clamp01(volume01);
        }
    }
}
