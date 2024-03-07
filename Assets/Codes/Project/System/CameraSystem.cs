using QFramework;
using UnityEngine;

namespace PlatformShoot
{
    public interface ICameraSystem : ISystem
    {
        void SetTarget(Transform transform);
        void Update();
    }
    public class CameraSystem: AbstractSystem,ICameraSystem
    {
        private Transform _mTarget;
        protected override void OnInit()
        {
            
        }

        public void SetTarget(Transform target)
        {
            _mTarget = target;
        }

        public void Update()
        {
            Camera.main.transform.localPosition = new Vector3(_mTarget.position.x, _mTarget.position.y, -10);
        }
    }
}