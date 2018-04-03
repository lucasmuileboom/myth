using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject _target;

    [Header("Components")]
    private Rigidbody2D _rb;
    private EnemyChase _chase;

    [Header("Numbers")]
    [SerializeField]
    private float _moveSpeed;
    private float _maxSpeed, _moveTimerMax;
    private int _moveTimer, _randomDir, _moveDir;

    [Header("bools")]
    private bool[] _rays = { false, false, false };
    private bool _climbing;
    private bool _inRange;

    void FixedUpdate()
    {
        if (_climbing)
        {
            print("climbing");
            Climb();
        }
        if (_inRange)
        {
            _moveDir = GetDir();
            _chase.ChaseObject(_moveDir);
        }
        else
        {
            Move();
        }
    }

    public void GetData(GameObject target, Rigidbody2D rb, EnemyChase chase, float maxSpeed, int moveTimerMax)
    {
        _target = target;
        _rb = rb;  
        _chase = chase;
        _maxSpeed = maxSpeed;
        _moveTimerMax = moveTimerMax;
    }

    public void GetSurroundings(bool[] rays, bool climbing, bool inRange)
    {
        _rays = new bool[rays.Length];
        for (int i = 0; i < rays.Length; i++)
        {
            _rays[i] = rays[i];
        }
        _climbing = climbing;
        _inRange = inRange;
    }
 
    void Move()
    {
        _moveTimer++;

        if (_moveTimer >= _moveTimerMax)
        {
            _randomDir = Random.Range(1, 4);
            _moveTimer = 0;
        }
        if ((!_rays[0] && _moveDir == -1) || (!_rays[2] && _moveDir == 1))
        {
            _moveSpeed = 0;
        }
        transform.position = new Vector2(transform.position.x + _moveSpeed * Time.deltaTime, transform.position.y);
        switch (_randomDir)
        {
            case 1:
                if (_rays[0])
                {
                    _moveSpeed = Mathf.Lerp(_moveSpeed, _maxSpeed, _maxSpeed / 20 * Time.deltaTime);
                    _moveDir = -1;
                }
                break;
            case 2:
                _moveSpeed = Mathf.Lerp(_moveSpeed, 0, _maxSpeed / 0 * Time.deltaTime);
                _moveDir = 0;
                break;
            case 3:
                if (_rays[2])
                {
                    _moveSpeed = Mathf.Lerp(_moveSpeed, -_maxSpeed, _maxSpeed / 20 * Time.deltaTime);
                    _moveDir = 1;
                }
                break;
        }
    }

    void Climb()
    {
        _rb.velocity += new Vector2(-_moveDir * 500, 20) * Time.deltaTime;
        _moveSpeed = 0;
    }

    int GetDir()
    {
        if (_target.transform.position.x > transform.position.x)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}