using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage
    {
        set
        {
            _damage = value;
        }
    }
    private float _damage;

    void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<EnemyHealth>().TakeDamage((int)_damage); 
        }
        Destroy(this.gameObject);
    }
}
