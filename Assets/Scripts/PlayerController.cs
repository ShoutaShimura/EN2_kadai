using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>速さ</summary>
    [SerializeField] float _movePower = 5f;
    /// <summary>ジャンプ力 </summary>
    [SerializeField] float _jumpPower = 5f;
    /// <summary>接地判定の長さ </summary>
    [SerializeField] float _isGroundedLength = 0.2f;
    Rigidbody _rb = default;
    /// <summary>入力された方向</summary>
    Vector3 _dir;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {

            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // カメラを基準で移動入力
            dir = Camera.main.transform.TransformDirection(dir);    
            dir.y = 0;  
            this.transform.forward = dir;   // ベクトルの方向にプレイヤー視点

            Vector3 velo = this.transform.forward * _movePower;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;
        }


        // 接地してたらジャンプ
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

        }
    }


    /// <summary>
    /// 地面との接触判定
    /// </summary>
    bool IsGrounded()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center; 
        Vector3 end = start + Vector3.down * (col.center.y + col.height / 2 + _isGroundedLength);
        Debug.DrawLine(start, end);
        bool isGrounded = Physics.Linecast(start, end);
        return isGrounded;
    }


    void FixedUpdate()
    {
        _rb.AddForce(_dir.normalized * _movePower);
    }
}
