using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the attack of the enemy,
/// it will hurt the player if they collide long enough
/// </summary>

public class EnemyAttack : MonoBehaviour
{
    [Header("Numbers")]
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private int _collisionTimer = 10;
    private int _collisionTime = 0;  

    public void Attack(GameObject target)
    {
        Debug.Log("Attacking NOW");
        target.gameObject.GetComponent<PlayerHealth>().TakeDamage(_damage);
        _collisionTime = 0;
    }
}
