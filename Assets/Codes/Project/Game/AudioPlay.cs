using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformShoot
{
    public class AudioPlay: MonoBehaviour
    {
        private List<AudioSource> _mPlayingList;
        public static AudioPlay Instance;

        private void Awake() => Instance = this;

        private void Start()
        {
            _mPlayingList = new List<AudioSource>();
            GameObject.DontDestroyOnLoad(gameObject);
        }
        
        public void PlaySound(string name)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = Resources.Load<AudioClip>(""+name);
            source.Play();
            _mPlayingList.Add(source);
        }

        public void Update()
        {
            //todo: 是否可以用这个表达式替换for循环？
            _mPlayingList.RemoveAll(source => !source.isPlaying);
            // for (int i = _mPlayingList.Count - 1; i > 0; i--)
            // {
            //     var source = _mPlayingList[i];
            //     if (source.isPlaying) continue;
            //     _mPlayingList.RemoveAt(i);
            //     Destroy(source);
            // }
        }
    }
}