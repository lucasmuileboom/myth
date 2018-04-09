using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will determine the movement of the enemy,
/// which includes checking the floor underneath, checking
/// whether it touches a wall and measuring distance between
/// itself and the player
/// </summary>

public class EnemyMovement : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject _player;

    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("Ints & Floats")]
    [SerializeField]
    private int _playerDirection;
    [SerializeField]
    private int _randomMove,
    _moveTimer,
    _attackDistance;
    [SerializeField]
    private float _moveSpeed,
    _moveSpeedMax,
    _scaleMultiplier;

    [Header("Bools")]
    [SerializeField]
    private bool _allowLeft;
    [SerializeField]
    private bool _allowRight,
    _playerInRange,
    _abovePlatform;  

    [Header("Vectors")]
    [SerializeField]
    private Vector2 _position;

	// Use this for initialization
	void Start ()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        _position = transform.position;
        if (_player.transform.position.x < transform.position.x)
        {
            _playerDirection = -1;
        }
        else
        {
            _playerDirection = 1;
        }
        if (_moveTimer > 90)
        {
            _randomMove = Random.Range(0, 3);
            _moveTimer = 0;
        }
        if (Vector2.Distance(transform.position, _player.transform.position) < _attackDistance)
        {
            _playerInRange = true;
        }
        else
        {
            _playerInRange = false;
        }
        if (_playerInRange)
        {
            if (!_allowLeft && _playerDirection == -1)
            {
                _moveSpeed = 0;
                transform.position = new Vector2(transform.position.x + 0.02f, transform.position.y);
            }
            else if (!_allowRight && _playerDirection == 1)
            {
                _moveSpeed = 0;
                transform.position = new Vector2(transform.position.x - 0.02f, transform.position.y);
            }
            else
            {
                _moveSpeed = Mathf.Lerp(_moveSpeed, _playerDirection * _moveSpeedMax * 1.5f, 0.01f);
            }
        }
        else
        {
            switch (_randomMove)
            {
                case 1:
                    if (_allowLeft)
                    {
                        if (_moveSpeed > -_moveSpeedMax)
                        {
                            _moveSpeed = Mathf.Lerp(_moveSpeed, -_moveSpeedMax * _scaleMultiplier, 0.01f);
                        }
                    }
                    else
                    {
                        _moveSpeed = 0;
                    }
                    break;
                case 2:
                    if (_allowRight)
                    {
                        if (_moveSpeed < _moveSpeedMax)
                        {
                            _moveSpeed = Mathf.Lerp(_moveSpeed, _moveSpeedMax * _scaleMultiplier, 0.01f);
                        }
                    }
                    else
                    {
                        _moveSpeed = 0;
                    }
                    break;
                default:
                    if (!_allowLeft)
                    {
                        _moveSpeed = 0;
                        transform.position = new Vector2(transform.position.x + 0.02f, transform.position.y);
                    }
                    else if (!_allowRight)
                    {
                        _moveSpeed = 0;
                        transform.position = new Vector2(transform.position.x - 0.02f, transform.position.y);
                    }
                    else
                    {
                        _moveSpeed = Mathf.Lerp(_moveSpeed, 0, 0.01f);
                    }
                    break;
            }
        }
        transform.Translate(Vector2.right * _moveSpeed);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.55f, transform.position.y - 0.25f), Vector2.down, 8f);
        Debug.DrawRay(new Vector2(transform.position.x - 0.55f, transform.position.y), Vector2.down, Color.red);
        if (hitLeft && hitLeft.collider.tag == "Platform")
        {
            if (hitLeft.point.y > transform.position.y - 0.45f)
            {
                _moveSpeed = 0;
                _rb.AddForce(new Vector2(0f, 15f));
            }
            else
            {
                _allowLeft = true;
            }
        }
        else
        {
            _allowLeft = false;
        }
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x + 0.55f, transform.position.y), Vector2.down, 8f);
        Debug.DrawRay(new Vector2(transform.position.x + 0.55f, transform.position.y), Vector2.down, Color.blue);
        if (hitRight && hitRight.collider.tag == "Platform")
        {
            if (hitRight.point.y > transform.position.y - 0.45f)
            {
                _moveSpeed = 0;
                _rb.AddForce(new Vector2(0f, 15f));
            }
            else
            {
                _allowRight = true;
            }
        }
        else
        {
            _allowRight = false;
        }
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.55f), Vector2.down, 7.45f);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.55f), Vector2.down, Color.green);
        if (hitDown && hitDown.collider.tag == "Platform")
        {
            _abovePlatform = true;
        }
        else
        {
            _abovePlatform = false;
        }
        _moveTimer++;
    }

    void LateUpdate()
    {
        if (!_abovePlatform)
        {
            Debug.Log("NOPE!");
            transform.position = _position;
        }
    }
}