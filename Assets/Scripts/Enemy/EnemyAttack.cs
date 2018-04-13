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

    public void Attack(GameObject target)
    {
        target.gameObject.GetComponent<PlayerHealth>().TakeDamage(_damage);
    }
}
