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
    private GameObject _player;
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
}
