
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
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyAttack))]

public class EnemyBase : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private GameObject _pickup;

    private Timer _moveTimer, _attackTimer;

    private BoxCollider2D _col;
    private Rigidbody2D _rb;
    private EnemyCheckSurroundings _checkSur;
    private EnemyMovement _movement;
    private EnemyChase _chase;
    private EnemyAttack _attack;

    [Header("Numbers")]
    [SerializeField]
    private float _sightRange;
    [SerializeField]
    private int _attackRange;
    private int _moveMin, _moveMax, _moveDir;
    [SerializeField]
    private float _moveSpeed, _maxSpeed, _walkSpeed, _runSpeed;
    [SerializeField]
    private float[] _rayLengths = {};

    [Header("Bools")]
    [SerializeField]
    private bool[] _rays = { };
    private bool _hasPickup, _attacking;

    [Header("Vectors")]
    [SerializeField]
    private Vector2[] _rayOffsets = {};

    // Use this for initialization
    void Start()
    {
        Physics2D.IgnoreCollision(_target.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        // First all components are acquired
        _col = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _checkSur = GetComponent<EnemyCheckSurroundings>();
        _movement = GetComponent<EnemyMovement>();
        _attack = GetComponent<EnemyAttack>();

        // Then data will be sent to the required components
        _rb.freezeRotation = true;
        _moveTimer = new Timer(2.5f);
        _attackTimer = new Timer(-1);
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (_checkSur.CheckDistance(_sightRange, _target))
        {
            _maxSpeed = _runSpeed;
            if (_target.transform.position.x < transform.position.x)
                _moveDir = 1;
            else
                _moveDir = -1;
        }
        else
            _maxSpeed = _walkSpeed;
        if (_checkSur.CheckDistance(_attackRange, _target))
        {
            _moveDir = 0;
            if (!_attacking)
                Attack();
        }
            
        if (_attacking && _attackTimer.Done())
        {
            print("Attacking");
            _attack.Attack(_target);
            _attacking = false;
        }

        for (int i = 0; i < _rays.Length; i++)
            _rays[i] = _checkSur.CheckPos(_rays[i], _rayOffsets[i], _rayLengths[i]);
        if (_rays[0])
            _moveMin = -1;
        else
            _moveMin = 0;
        if (_rays[2])
            _moveMax = 2;
        else
            _moveMax = 1;
        if (_moveTimer.Done())
            Move();
        transform.position = _movement.Move(_moveSpeed);

        switch (_moveDir)
        {
            case -1:
                if (_hasPickup)
                    _moveSpeed = Mathf.Lerp(_moveSpeed, -_maxSpeed, 0.1f * Time.deltaTime);
                else
                    _moveSpeed = Mathf.Lerp(_moveSpeed, _maxSpeed, 0.1f * Time.deltaTime);
                break;
            case 0:
                _moveSpeed = Mathf.Lerp(_moveSpeed, 0, 0.5f * Time.deltaTime);
                break;
            case 1:
                if (_hasPickup)
                    _moveSpeed = Mathf.Lerp(_moveSpeed, _maxSpeed, 0.1f * Time.deltaTime);
                else
                    _moveSpeed = Mathf.Lerp(_moveSpeed, -_maxSpeed, 0.1f * Time.deltaTime);
                break;
        }

        if ((_moveSpeed < 0 && !_rays[0]) || (_moveSpeed > 0 && !_rays[2]))
            _moveSpeed = 0;
    }

    void OnDestroy()
    {
        if (_hasPickup)
        {
            GameObject p = Instantiate<GameObject>(_pickup);
            p.transform.position = this.transform.position;
        }
    }

    void Move()
    {
        _moveTimer = new Timer(1);
        _moveDir = Random.Range(_moveMin, _moveMax);
        print(_moveDir);
    }

    void Attack()
    {
        _attacking = true;
        _attackTimer = new Timer(1);
    }
}