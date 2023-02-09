using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour
{
    [SerializeField] float _movePower = 6f;
    [SerializeField] float _jumpPower = 6f;

    Animator _anim = default;
    Rigidbody _rb = default;
 
    bool _isGrounded;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
      //移動
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;

        //キャラクターの向き
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
        }

        Vector3 velocity = dir.normalized * _movePower;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;

        // ジャンプ
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    void LateUpdate()
    {
        // アニメーションの処理
        if (_anim)
        {
            _anim.SetBool("IsGrounded", _isGrounded);
            Vector3 Speed = _rb.velocity;
            Speed.y = 0;
            _anim.SetFloat("Speed", Speed.magnitude);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }

    void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }
}
