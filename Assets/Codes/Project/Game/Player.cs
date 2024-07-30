using UnityEngine;
using QFramework;

namespace PlatformShoot
{
    /**
     * 玩家类，负责玩家角色的控制和行为。
     * 实现了IController接口以接入框架。
     */
    public class Player : MonoBehaviour, IController
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        // 玩家刚体组件，用于物理模拟和控制。
        private Rigidbody2D _mRig;
        // 玩家的盒形碰撞器，用于检测与环境的碰撞。
        private BoxCollider2D _mCollBox;
        // 地面层的遮罩，用于检测玩家是否站在地面上。
        private LayerMask _mGroundLayerMask;
        // 加速和减速的速率。
        private float _mAccDelta = 0.6f;
        private float _mDecDelta = 0.9f;
        // 地面移动速度。
        private float _mGroundMoveSpeed = 5f;
        // 跳跃力。
        private float _mJumpForce = 12f;
        // 是否输入了跳跃指令。
        private bool _mJumpInput;
        // 玩家面向的方向。
        private float _mFaceDir = 1;
        // 是否正在跳跃。
        private bool _isJumping = true;

        /**
         * 初始化玩家组件。
         * 设置刚体和碰撞器，配置地面检测的参数，并初始化相机系统的目标为玩家。
         */
        private void Start()
        {
            _mRig = GetComponent<Rigidbody2D>();
            _mCollBox = GetComponentInChildren<BoxCollider2D>();
            _mGroundLayerMask = LayerMask.GetMask("Ground");
            this.GetSystem<ICameraSystem>().SetTarget(transform);
        }

        /**
         * 每帧更新玩家状态。
         * 处理输入，检测是否站在地面上，以及播放相应的音效。
         */
        private void Update()
        {
            // 处理攻击输入。
            if (Input.GetKeyDown(KeyCode.J))
            {
                //播放攻击音效
                AudioPlay.Instance.PlaySound("竖琴");
                //生成子弹
                var obj = Resources.Load<GameObject>("Item/Bullet");
                obj = Instantiate(obj, transform.position ,Quaternion.identity);
                var bullet = obj.GetComponent<Bullet>();
                // bullet.GetGamePass(_gamePass);
                bullet.InitDir(_mFaceDir);
            }
            //根据相交盒判定角色是否处于地面
            var ground = Physics2D.OverlapBox(transform.position + Vector3.down * _mCollBox.size.y * 0.5f, new Vector2(_mCollBox.size.x*0.8f,0.1f),0,_mGroundLayerMask);
            if (ground)
            {
                if (_isJumping)
                {
                    //播放落地声音
                    AudioPlay.Instance.PlaySound("落地2");
                    _isJumping = false;
                }
                // 处理跳跃输入。
                if (Input.GetKeyDown(KeyCode.K))
                {
                    //播放跳跃的声音
                    AudioPlay.Instance.PlaySound("跳跃");
                    _mJumpInput = true;
                    _isJumping = true;
                }
            }

            // 处理移动输入和转向。
            //判断角色转向
            float h = Input.GetAxisRaw("Horizontal");
            if ((h != 0) && (h != _mFaceDir))
            {
                _mFaceDir = -_mFaceDir;
                transform.Rotate(0, 180, 0);
            }
        }
        
        /**
         * 平滑移动函数。
         * 根据输入调整玩家的水平速度。
         */
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

        /**
         * 在编辑器中绘制辅助图形。
         * 用于显示用于地面检测的盒形区域。
         */
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + _mCollBox.size.y * Vector3.down * 0.5f, new Vector2(_mCollBox.size.x*0.8f,0.1f));
        }

        /**
         * 物理系统中的固定更新。
         * 处理跳跃逻辑和移动。
         */
        private void FixedUpdate()
        {
            if (_mJumpInput)
            {
                _mJumpInput = false;
                _mRig.velocity = new Vector2(_mRig.velocity.x, _mJumpForce);
            }
            float h = Input.GetAxisRaw("Horizontal");
            SmoothMove(h);
        }

        /**
         * 当玩家碰撞到特定物体时的处理。
         * 处理进入下一关和收集奖励的逻辑。
         */
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Door"))
            {
                AudioPlay.Instance.PlaySound("通关音效");
                this.SendCommand<NextLevelCommand>(new NextLevelCommand("GamePassScene"));
            }
            if (col.gameObject.CompareTag("Reward"))
            {
                this.GetModel<IGameModel>().Score.Value++;
                AudioPlay.Instance.PlaySound("拾取金币");
                //_mainPanel.UpdateScoreText();
                Destroy(col.gameObject);
            }
        }

        /**
         * 获取玩家所属的架构。
         * 用于框架内部的依赖注入和事件分发。
         */
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PlatformShootGame.Interface;
        }
    }
}
