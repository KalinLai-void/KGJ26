using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nori
{
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "Nori/Audio Library", order = 1)]
    public class AudioLibrary : ScriptableObject
    {
        [Serializable]
        public class SfxEntry
        {
            public SfxId id;
            public AudioClip clip;
        }

        [SerializeField] private SfxEntry[] _entries;

        private Dictionary<SfxId, AudioClip> _map;

        private void OnEnable()
        {
            RebuildMap();
        }

        private void OnValidate()
        {
            RebuildMap();
        }

        private void RebuildMap()
        {
            _map = new Dictionary<SfxId, AudioClip>();
            if (_entries == null)
                return;

            foreach (var e in _entries)
            {
                if (e.clip == null)
                    continue;
                _map[e.id] = e.clip;
            }
        }

        public AudioClip Get(SfxId id)
        {
            if (_map == null)
                RebuildMap();

            return _map != null && _map.TryGetValue(id, out var c) ? c : null;
        }

        public bool TryGet(SfxId id, out AudioClip clip)
        {
            clip = Get(id);
            return clip != null;
        }

        public void PlaySfx(SfxId id, float volumeScale = 1f)
        {
            var clip = Get(id);
            if (clip != null)
                AudioManager.PlaySFX(clip, volumeScale);
        }
    }
}
