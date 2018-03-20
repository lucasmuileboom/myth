using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerMovement _PlayerMovement;
    [SerializeField]private LayerMask enemysLayer;
    [SerializeField]private AttackStats[] Attacks = new AttackStats[2];
    [System.Serializable]
    private class AttackStats
    {
        public float FireRate;
        public float Range;
        public int Damage;
    }
    private Vector2 direction;
    private float _timer = 0;

    void Start ()
    {
        _inputManager = GetComponent<InputManager>();
        _PlayerMovement = GetComponent<PlayerMovement>();
    }
	void Update ()
    {
        _timer -= Time.deltaTime;
        if (_inputManager.lightAttack())
        {
            Attack(0);
            
        }
        if (_inputManager.heavyAttack())
        {
            Attack(1);
        }
    }
    void Attack(int i)
    {
        if (_timer < 0)
        {
            if (_PlayerMovement.IsFlipped())
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
            }
            Debug.DrawRay(transform.position, direction * Attacks[i].Range, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Attacks[i].Range, enemysLayer);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(Attacks[i].Damage, direction);
                print("hit");
            }
            else
            {
                print("miss");
            }
            _timer = Attacks[i].FireRate;
        }
    }
}
