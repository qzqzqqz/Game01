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
                var bullet = Resources.Load<GameObject>("Bullet");
                bullet = Instantiate(bullet, transform.position ,Quaternion.identity);
                bullet.GetComponent<Bullet>().GetGamePass(_gamePass);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                mJumpInput = true;
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
