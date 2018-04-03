using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    private bool IsGrounded()
    {
        float distance = 0.7f;
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<InputManager>();
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
        _moveSpeedCurrent = 0;
    }
    private void Left()
    {
        _isFlipped = true;
        _moveSpeedCurrent -= _Speed;
        if (_moveSpeedCurrent <= -_SpeedMax)
        {
            _moveSpeedCurrent = -_SpeedMax;
        }
    }
    private void Right()
    {
        _isFlipped = false;
        _moveSpeedCurrent += _Speed;
        if (_moveSpeedCurrent >= _SpeedMax)
        {
            _moveSpeedCurrent = _SpeedMax;
        }
    }
    private void DodgeLeft()
    {
        _isFlipped = true;
        _timer = _dodgeCoolDown;
        _moveSpeedCurrent -= _dodgeSpeed;
        print("dodgeL");
    }
    private void DodgeRight()
    {
        _isFlipped = false;
        _timer = _dodgeCoolDown;
        _moveSpeedCurrent += _dodgeSpeed;
        print("dodgeR");
    }
    private void ResetJump()
    {
        _jump = false;
        _douleJump = false;
        _jumpForceCurrent = 0;
    }
    private IEnumerator Jump()
    {
        print("j");
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
        print("dj");
        _jumpForceCurrent += _doubleJumpForce;
        _douleJump = true;
    }
    private void Gravity()
    {
        _jumpForceCurrent -= _gravity;
    }
}
