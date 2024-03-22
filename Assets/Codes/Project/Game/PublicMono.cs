using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformShoot
{
    public class PublicMono : MonoBehaviour
    {
        public static PublicMono Instance;

        private void Awake()
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        public event Action OnUpdate;
        public event Action OnLateUpdate;
        public event Action OnFixedUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }

}