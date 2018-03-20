using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the attack of the enemy,
/// it will hurt the player if they collide long enough
/// </summary>

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int _collisionTimer = 10;
    private int _collisionTime = 0;
    [SerializeField]
    private int _damage = 1;

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            _collisionTime ++;
        }
        if (_collisionTime >= _collisionTimer)
        {
            Debug.Log("Attacking NOW");
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(_damage);
            _collisionTime = 0;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            _collisionTime = 0;
        }
    }


}
