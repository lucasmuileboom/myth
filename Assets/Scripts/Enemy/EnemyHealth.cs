using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _knockback;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 direction)
    {
        if (this.tag == "Enemy")
        {
            _rb.AddForce(direction * _knockback * 100);
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
}
