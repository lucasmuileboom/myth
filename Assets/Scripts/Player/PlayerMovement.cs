﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer;
    private Animator _Animator;
    private InputManager _inputManager;
    private Rigidbody2D _rigidbody;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private int _SpeedMax;
    [SerializeField]private int _Speed;
    [SerializeField]private int _dodgeSpeed;
    [SerializeField]private float _dodgeCoolDown;
    [SerializeField]private int _jumpForce;
    [SerializeField]private float maxJumpForce;
    [SerializeField]private int _doubleJumpForce;
    [SerializeField]private float _gravity;
    private  Vector2 _moveVector;
    private  Vector2 _jumpReset;
    private float _jumpForceCurrent;
    private float _moveSpeedCurrent;
    private float _timer;
    private bool  _jump = false;
    private bool _holdjump= false;
    private bool _douleJump = false;
    private bool _isFlipped;
    public bool IsFlipped()
    {
        return _isFlipped;
    }
    public bool IsGrounded()
    {
        float distance = 0.1f;
        Debug.DrawRay(transform.position + new Vector3(0.8f, 0, 0), Vector2.down * distance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.8f,0,0), Vector2.down, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        Debug.DrawRay(transform.position - new Vector3(0.9f, 0, 0), Vector2.down * distance, Color.green);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position - new Vector3(0.9f, 0, 0), Vector2.down, distance, groundLayer);
        if (hit1.collider != null)
        {
            return true;
        }
        return false;

    }
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<InputManager>();
        _Animator = GetComponent<Animator>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (!_inputManager.left() && !_inputManager.right() || _inputManager.left() && _inputManager.right())
        {
            Idle();
        }
        else if (_inputManager.dodge() && _inputManager.right() && _timer <= 0)
        {
            DodgeRight();
        }
        else if (_inputManager.dodge() && _inputManager.left() && _timer <= 0)
        {
            DodgeLeft();
        }
        else if (_inputManager.left())
        {
            Left();
        }
        else if (_inputManager.right())
        {
            Right();
        }
        if (!IsGrounded())
        {
            Gravity();
        }
        else if (IsGrounded())
        {
            ResetJump();
        }
        if (_jump && !_holdjump && !_douleJump && !IsGrounded() && _inputManager.jump())
        {
            DoubleJump();
        }
        else if (!_jump && IsGrounded() && _inputManager.jump())
        {
            StartCoroutine(Jump());
        }
        _timer -= Time.deltaTime;
        _moveVector = new Vector2(_moveSpeedCurrent, _jumpForceCurrent);
        _rigidbody.velocity = _moveVector/* * Time.deltaTime*/;
    }
    private void Idle()
    {
        _Animator.SetBool("run", false);
        _moveSpeedCurrent = 0;
    }
    private void Left()
    {
        _Animator.SetBool("run", true);
        _SpriteRenderer.flipX = true;
        _isFlipped = true;
        _moveSpeedCurrent -= _Speed;
        if (_moveSpeedCurrent <= -_SpeedMax)
        {
            _moveSpeedCurrent = -_SpeedMax;
        }
    }
    private void Right()
    {
        _Animator.SetBool("run", true);
        _SpriteRenderer.flipX = false;
        _isFlipped = false;
        _moveSpeedCurrent += _Speed;
        if (_moveSpeedCurrent >= _SpeedMax)
        {
            _moveSpeedCurrent = _SpeedMax;
        }
    }
    private void DodgeLeft()
    {
        _SpriteRenderer.flipX = true;
        _isFlipped = true;
        _timer = _dodgeCoolDown;
        _moveSpeedCurrent -= _dodgeSpeed;
    }
    private void DodgeRight()
    {
        _SpriteRenderer.flipX = false;
        _isFlipped = false;
        _timer = _dodgeCoolDown;
        _moveSpeedCurrent += _dodgeSpeed;
    }
    private void ResetJump()
    {
        _Animator.SetBool("jump", false);
        _jump = false;
        _douleJump = false;
        _jumpForceCurrent = 0;
    }
    private IEnumerator Jump()
    {
        _Animator.SetBool("jump", true);
        _jump = true;
        _holdjump = true;
        float startpositionY = transform.position.y;
        while (_inputManager.holdjump() && this.transform.position.y < startpositionY + maxJumpForce)
        {
            _jumpForceCurrent = _jumpForce;
            yield return null;
        }
        _holdjump = false;
    }
    private void DoubleJump()
    {
        _jumpForceCurrent += _doubleJumpForce;
        _douleJump = true;
    }
    private void Gravity()
    {
        _jumpForceCurrent -= _gravity;
    }
}
