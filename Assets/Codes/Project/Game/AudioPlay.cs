using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformShoot
{
    public class AudioPlay: MonoBehaviour
    {
        //播放列表
        private List<AudioSource> _mPlayingList;
        //对外暴露一个静态实例
        public static AudioPlay Instance;

        private void Awake() => Instance = this;

        //在start中初始化播放列表 
        private void Start()
        {
            _mPlayingList = new List<AudioSource>();
            //在start中做一个转换保护，这样过场景时不会被切断
            DontDestroyOnLoad(gameObject);
        }
        
        public void PlaySound(string name)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = Resources.Load<AudioClip>("Audio/Sound/"+name);
            source.Play();
            _mPlayingList.Add(source);
        }

        public void Update()
        {
            for (int i = _mPlayingList.Count - 1; i > 0; i--)
            {
                var source = _mPlayingList[i];
                if (source.isPlaying) continue;
                _mPlayingList.RemoveAt(i);
                Destroy(source);
            }
        }
    }
}