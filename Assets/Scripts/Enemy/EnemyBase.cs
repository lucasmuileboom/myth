
using UnityEngine;

/// <summary>
/// This script will be the brain of the enemy, and is
/// responsible for sending data between the linked scripts.
/// The entire enemy will be ajustable from here.
/// </summary>

/*
 * First require all components of the enemy, so that
 * adding this script alone will be enough for creating
 * the entire enemy
*/

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyCheckSurroundings))]
[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAttack))]

public class EnemyBase : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField]
    private GameObject _target; 

    [Header("Components")]
    private BoxCollider2D _col;
    private Rigidbody2D _rb;
    private EnemyCheckSurroundings _checkSur;
    private EnemyMove _movement;
    private EnemyChase _chase;
    private EnemyAttack _attack;

    [Header("Numbers")]
    [SerializeField]
    private int _targetDistance;
    [SerializeField]
    private int _moveTimerMax;
    [SerializeField]
    private float _moveSpeed, _maxSpeed;
    [SerializeField]
    private float[] _rayLengths = {};

    [Header("Bools")]
    [SerializeField]
    private bool[] _rays = { };
    [SerializeField]
    private bool _fleeing;
    [SerializeField]private bool _inRange, _climbing;

    [Header("Vectors")]
    [SerializeField]
    private Vector2[] _rayOffsets = {};

    // Use this for initialization
    void Start ()
    {
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
        // First all components are acquired
        _col = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _checkSur = GetComponent<EnemyCheckSurroundings>();
        _movement = GetComponent<EnemyMove>();
        _chase = GetComponent<EnemyChase>();
        _attack = GetComponent<EnemyAttack>();

        // Then data will be sent to the required components
        _rb.freezeRotation = true;
        _checkSur.GetData(_target, _targetDistance);
        _movement.GetData(_target, _rb, _chase, _maxSpeed, _moveTimerMax);
        _chase.GetData(_maxSpeed, _fleeing, _target);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        _inRange = _checkSur.CheckDistance();
        for (int i = 0; i < _rays.Length; i++)
        {
            _rays[i] = _checkSur.CheckPos(_rays[i], _rayOffsets[i], _rayLengths[i]);
        }
        _movement.GetSurroundings(_rays, _climbing, _inRange);
	}

    public void Climbing (bool c)
    {
        _climbing = c;
    }

    void LateUpdate ()
    {
        _climbing = false;
    }
}
/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will determine the movement of the enemy,
/// which includes checking the floor underneath, checking
/// whether it touches a wall and measuring distance between
/// itself and the player
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(EnemyMove))]

public class EnemyMovement : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject target;

    [Header("Components")]
    private Rigidbody2D _rb;
    private EnemyChase _chase;
    //private EnemyMove _move;

    [Header("Ints & Floats")]
    [SerializeField]
    private int _targetDirection;
    [SerializeField]
    private int _randomMove, _moveTimer, _moveTimerMax, _attackDistance;
    public float moveSpeed, moveSpeedMax;
    [SerializeField]
    private float _scaleMultiplier;

    [Header("Bools")]
    [SerializeField]
    private bool _allowLeft;
    [SerializeField]
    private bool _allowRight, _playerInRange, _abovePlatform, _hasPickup;

    [Header("Vectors")]
    [SerializeField]
    private Vector2 _position;

    [Header("States")]
    private States _state;

    private enum States
    {
        Normal, Chase
    }

	void Start ()
    {
        _rb = GetComponent<Rigidbody2D>();
        _chase = this.GetComponent<EnemyChase>();
        _state = States.Normal;
	}
	
	void FixedUpdate ()
    {

        if (_moveTimer > _moveTimerMax)
        {
            _randomMove = Random.Range(0, 3);
            _moveTimer = 0;
        }
        switch (_state)
        {
            case (States.Chase):
                _chase.Chase(_allowLeft, _allowRight, _hasPickup, -_targetDirection);
                break;
            case (States.Normal):
                break;
            default:
                break;
        }
        if (_playerInRange)
        {
            _state = States.Chase;
        }
        else
        {
            _state = States.Normal;

            switch (_randomMove)
            {
                case 1:
                    if (_allowLeft)
                    {
                        if (moveSpeed > -moveSpeedMax)
                        {
                            moveSpeed = Mathf.Lerp(moveSpeed, -moveSpeedMax * _scaleMultiplier, 0.01f);
                        }
                    }
                    else
                    {
                        moveSpeed = 0;
                    }
                    break;
                case 2:
                    if (_allowRight)
                    {
                        if (moveSpeed < moveSpeedMax)
                        {
                            moveSpeed = Mathf.Lerp(moveSpeed, moveSpeedMax * _scaleMultiplier, 0.01f);
                        }
                    }
                    else
                    {
                        moveSpeed = 0;
                    }
                    break;
                default:
                    if (!_allowLeft)
                    {
                        moveSpeed = 0;
                        transform.position = new Vector2(transform.position.x + 0.02f, transform.position.y);
                    }
                    else if (!_allowRight)
                    {
                        moveSpeed = 0;
                        transform.position = new Vector2(transform.position.x - 0.02f, transform.position.y);
                    }
                    else
                    {
                        moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.01f);
                    }
                    break;
            }
        }
        _position = transform.position;
        if (target.transform.position.x < transform.position.x)
        {
            _targetDirection = -1;
        }
        else
        {
            _targetDirection = 1;
        }

        if (Vector2.Distance(transform.position, target.transform.position) < _attackDistance)
        {
            _playerInRange = true;
        }
        else
        {
            _playerInRange = false;
        }
        transform.Translate(Vector2.right * moveSpeed);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.55f, transform.position.y - 0.25f), Vector2.down, 8f);
        Debug.DrawRay(new Vector2(transform.position.x - 0.55f, transform.position.y), Vector2.down, Color.red);
        if (hitLeft && hitLeft.collider.tag == "Platform")
        {
            if (hitLeft.point.y > transform.position.y - 0.45f)
            {
                moveSpeed = 0;
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
                moveSpeed = 0;
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
*/
