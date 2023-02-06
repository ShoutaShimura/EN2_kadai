using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>����</summary>
    [SerializeField] float _movePower = 5f;
    /// <summary>�W�����v�� </summary>
    [SerializeField] float _jumpPower = 5f;
    /// <summary>�ڒn����̒��� </summary>
    [SerializeField] float _isGroundedLength = 0.2f;
    Rigidbody _rb = default;
    /// <summary>���͂��ꂽ����</summary>
    Vector3 _dir;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // �����̓��͂��擾���A���������߂�
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {

            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            // �J��������ňړ�����
            dir = Camera.main.transform.TransformDirection(dir);    
            dir.y = 0;  
            this.transform.forward = dir;   // �x�N�g���̕����Ƀv���C���[���_

            Vector3 velo = this.transform.forward * _movePower;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;
        }


        // �ڒn���Ă���W�����v
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

        }
    }


    /// <summary>
    /// �n�ʂƂ̐ڐG����
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
