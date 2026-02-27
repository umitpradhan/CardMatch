using System.Collections.Generic;
using UnityEngine;

namespace CardMatch.Services.AudioServices
{
    public sealed class AudioService : MonoBehaviour
    {
        [System.Serializable]
        private struct AudioMapping
        {
            public AudioEvent Event;
            public AudioClip Clip;
        }

        [SerializeField] private AudioMapping[] _clips;
        [SerializeField] private int _poolSize = 5;

        private Dictionary<AudioEvent, AudioClip> _lookup;
        private Queue<AudioSource> _sources;

        private void Awake()
        {
            _lookup = new Dictionary<AudioEvent, AudioClip>();
            foreach (var map in _clips)
                _lookup[map.Event] = map.Clip;

            _sources = new Queue<AudioSource>();
            for (int i = 0; i < _poolSize; i++)
            {
                var src = gameObject.AddComponent<AudioSource>();
                _sources.Enqueue(src);
            }
        }

        public void Play(AudioEvent evt)
        {
            if (!_lookup.TryGetValue(evt, out var clip))
                return;

            var source = _sources.Dequeue();
            source.PlayOneShot(clip);
            _sources.Enqueue(source);
        }
    }
}