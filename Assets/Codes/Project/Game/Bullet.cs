                                                                                                                                                                                                    using System;
using Unity.VisualScripting;
using UnityEngine;
using QFramework;
namespace PlatformShoot
{
    public class Bullet : MonoBehaviour,IController
    {
        private LayerMask _layerMask;
        // private GameObject _gamePass;
        private float _dir;

        // public void GetGamePass(GameObject gamePass)
        // {
        //     _gamePass = gamePass;
        // }
        
        //初始化方向
        public void InitDir(float dir)
        {
            _dir = dir;
        }

        // Start is called before the first frame update
        private void Start()
        {
            Destroy(gameObject, 2f);
            _layerMask = LayerMask.GetMask("Ground", "Trigger");
        }

        // Update is called once per frame
        private void Update()
        {
            transform.Translate(_dir* 12 * Time.deltaTime, 0, 0);
        }

        private void FixedUpdate()
        {
            var col = Physics2D.OverlapBox(transform.position, transform.localScale, 0, _layerMask);
            if (col)
            {
                if (col.CompareTag("Trigger"))
                {
                    Destroy(col.gameObject);
                    this.SendCommand<ShowPassDoorCommand>();
                }
                Destroy(gameObject);
            }
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PlatformShootGame.Interface;
        }
    }
}