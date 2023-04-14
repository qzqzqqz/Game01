using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformShoot
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D mRig;
        private float mGroundMoveSpeed = 5f;
        private float mJumpForce = 12f;
        private bool mJumpInput;
        private MainPanel _mainPanel;
        private GameObject _gamePass;
        private float _mFaceDir = 1;

        // Start is called before the first frame update
        private void Start()
        {
            mRig = GetComponent<Rigidbody2D>();
            _mainPanel = GameObject.Find("MainPanel").GetComponent<MainPanel>();
            _gamePass = GameObject.Find("GamePass");
            _gamePass.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                var obj = Resources.Load<GameObject>("Bullet");
                obj = Instantiate(obj, transform.position ,Quaternion.identity);
                var bullet = obj.GetComponent<Bullet>();
                bullet.GetGamePass(_gamePass);
                bullet.InitDir(_mFaceDir);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                mJumpInput = true;
            }
            //判断角色转向
            if ((Input.GetAxisRaw("Horizontal") != 0) && (Input.GetAxisRaw("Horizontal") != _mFaceDir))
            {
                _mFaceDir = -_mFaceDir;
                transform.Rotate(0, 180, 0);
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (mJumpInput)
            {
                mJumpInput = false;
                mRig.velocity = new Vector2(mRig.velocity.x, mJumpForce);
            }
            mRig.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * mGroundMoveSpeed, mRig.velocity.y);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Door"))
            {
                SceneManager.LoadScene("GamePassScene");
            }

            if (col.gameObject.CompareTag("Reward"))
            {
                _mainPanel.UpdateScoreText();
                Destroy(col.gameObject);
            }
        }
    }
}
