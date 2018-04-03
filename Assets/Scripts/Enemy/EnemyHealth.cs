using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject _player;

    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("Numbers")]
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _knockback;
    
    [Header("Vectors")]
    private Vector2 _direction;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int damage)
    {

        if (this.tag == "Enemy")
        {
            if (_player.transform.position.x < transform.position.x)
            {
                _direction = Vector2.right;
            }
            else
            {
                _direction = Vector2.left;
            }

            _rb.AddForce(_direction * _knockback * 100);
        }
        else
        {
            GetComponent<BossMovement>().SlowDown();
        }
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public int GetHealth()
    {
        return _health;
    }
}
