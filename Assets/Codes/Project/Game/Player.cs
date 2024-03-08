using System;
using UnityEngine;
using QFramework;

namespace PlatformShoot
{
    public class Player : MonoBehaviour, IController
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        private Rigidbody2D _mRig;
        private BoxCollider2D _mCollBox;
        private LayerMask _mGroundLayerMask;
        private float _mAccDelta = 0.6f;
        private float _mDecDelta = 0.9f;
        private float _mGroundMoveSpeed = 5f;
        private float _mJumpForce = 12f;
        private bool _mJumpInput;
        private float _mFaceDir = 1;

        // Start is called before the first frame update
        private void Start()
        {
            _mRig = GetComponent<Rigidbody2D>();
            _mCollBox = GetComponentInChildren<BoxCollider2D>();
            _mGroundLayerMask = LayerMask.GetMask("Ground");
            this.GetSystem<ICameraSystem>().SetTarget(transform);

            // _mainPanel = GameObject.Find("MainPanel").GetComponent<MainPanel>();
            // _gamePass = GameObject.Find("GamePass");
            // _gamePass.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                var obj = Resources.Load<GameObject>("Item/Bullet");
                obj = Instantiate(obj, transform.position ,Quaternion.identity);
                var bullet = obj.GetComponent<Bullet>();
                // bullet.GetGamePass(_gamePass);
                bullet.InitDir(_mFaceDir);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (Physics2D.OverlapBox(transform.position + Vector3.down * _mCollBox.size.y * 0.5f, new Vector2(_mCollBox.size.x*0.8f,0.1f),0,_mGroundLayerMask))
                {
                    _mJumpInput = true;
                }
            }
            //判断角色转向
            float h = Input.GetAxisRaw("Horizontal");
            if ((h != 0) && (h != _mFaceDir))
            {
                _mFaceDir = -_mFaceDir;
                transform.Rotate(0, 180, 0);
            }
        }
        
        //平滑移动
        private void SmoothMove(float input)
        {
            if (input != 0)
            {
                _mRig.velocity =
                    new Vector2(Mathf.Clamp(_mRig.velocity.x + input * _mAccDelta, -_mGroundMoveSpeed, _mGroundMoveSpeed),
                        _mRig.velocity.y);
            }
            else
            {
                _mRig.velocity = new Vector2(Mathf.MoveTowards(_mRig.velocity.x, 0, _mDecDelta), _mRig.velocity.y);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + _mCollBox.size.y * Vector3.down * 0.5f, new Vector2(_mCollBox.size.x*0.8f,0.1f));
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (_mJumpInput)
            {
                _mJumpInput = false;
                _mRig.velocity = new Vector2(_mRig.velocity.x, _mJumpForce);
            }
            float h = Input.GetAxisRaw("Horizontal");
            SmoothMove(h);
            // mRig.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _mGroundMoveSpeed, mRig.velocity.y);
        }

        private void LateUpdate()
        {
            this.GetSystem<ICameraSystem>().Update();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Door"))
            {
                this.SendCommand<NextLevelCommand>(new NextLevelCommand("GamePassScene"));
            }
            if (col.gameObject.CompareTag("Reward"))
            {
                this.GetModel<IGameModel>().Score.Value++;
                //_mainPanel.UpdateScoreText();
                Destroy(col.gameObject);
            }
        }


        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PlatformShootGame.Interface;
        }
    }
}
