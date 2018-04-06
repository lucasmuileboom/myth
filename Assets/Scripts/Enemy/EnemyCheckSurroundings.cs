using UnityEngine;

public class EnemyCheckSurroundings : MonoBehaviour
{

    [Header("GameObjects")]
    private GameObject _target;

    [Header("Components")]
    private EnemyBase _base;
    private EnemyAttack _attack;

    [Header("Numbers")]
    [SerializeField]
    private int _attackMax;
    private int _distance, _attackTimer = 0;

    void Start()
    {
        _base = GetComponent<EnemyBase>();
        _attack = GetComponent<EnemyAttack>();
    }

    public bool CheckDistance()
    {
        if (Vector2.Distance(transform.position, _target.transform.position) <= _distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckPos(bool ray, Vector2 offset, float length)
    {
        offset = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
        RaycastHit2D hit = Physics2D.Raycast(offset, Vector2.down, length);
        Debug.DrawRay(offset, Vector2.down * length, Color.red);
        if (hit && hit.collider.tag == "Player")
        {
            _attackTimer++;
            if (_attackTimer >= _attackMax)
            {
                _attack.Attack(_target);
                _attackTimer = 0;
            }
        }
        if (hit && hit.collider.tag == "Platform")
        {
            if (hit.point.y > transform.position.y - 0.5f)
            {
                _base.Climbing(true);
            }
            else
            {
                ray = true;
            }
        }
        else
        {
            ray = false;
        }
        return ray;
    }

    public void GetData(GameObject target, int distance)
    {
        _target = target;
        _distance = distance;
    }
}