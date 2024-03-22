using QFramework;
using UnityEngine;

namespace PlatformShoot
{
    public interface ICameraSystem : ISystem
    {
        void SetTarget(Transform transform);
    }
    public class CameraSystem: AbstractSystem,ICameraSystem
    {
        private Transform _mTarget;
        protected override void OnInit()
        {
            PublicMono.Instance.OnLateUpdate += Update;
        }

        public void SetTarget(Transform target)
        {
            _mTarget = target;
        }

        private void Update()
        {
            if (_mTarget == null) return;
            var mTargetPosition = _mTarget.position;
            Camera.main.transform.localPosition = new Vector3(mTargetPosition.x, mTargetPosition.y, -10);
        }
    }
}