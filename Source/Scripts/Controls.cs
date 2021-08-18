using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpHeight = 4;
    [SerializeField] private bool _canJump = true;

    private Rigidbody _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("_rb is null!");
        }
    }

    void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Left,Right"), 0f, Input.GetAxis("Front,Back")) * (_speed * Time.deltaTime));
        
        JumpCheck();
    }

    void JumpCheck()
    {
        if (Input.GetAxis("Jump") > 0.1 && _canJump)
        {
            _rb.AddForce(Vector3.up * _jumpHeight);
            _canJump = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        _canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
