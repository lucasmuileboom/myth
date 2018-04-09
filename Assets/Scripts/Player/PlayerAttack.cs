using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerMovement _PlayerMovement;
    [SerializeField]private LayerMask _enemysLayer;
    [SerializeField]private GameObject _projectile;
    [SerializeField] private float _yOffset;
    [SerializeField]private float _minProjectileSpeed;
    [SerializeField]private float _maxProjectileSpeed;
    [SerializeField]private float _maxProjectileDamage;
    [SerializeField]private int _chargeSpeed;
    [SerializeField]private AttackStats[] Attacks = new AttackStats[3];
    [System.Serializable]
    private class AttackStats
    {
        public float FireRate;
        public float Range;
        public int Damage;
    }
    private Vector2 direction;
    private Vector3 _offset;
    private float _timer = 0;
    private float _projectileSpeed;
    private float _projectileDamage;
    private bool _chargeAttack;

    void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _PlayerMovement = GetComponent<PlayerMovement>();
        _offset = new Vector3(0, _yOffset,0);
    }
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_inputManager.lightAttack())
        {
            Attack(0, true);

        }
        else if (_inputManager.heavyAttack())
        {
            Attack(1, true);
        }
        else if (_inputManager.rangeAttack())
        {
            Attack(2, false);
        }
    }
    private void Attack(int i, bool melee)
    {
        if (_timer < 0 && !_chargeAttack)
        {
            if (_PlayerMovement.IsFlipped())
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
            }
            if (melee)
            {
                Debug.DrawRay(transform.position + _offset, direction * Attacks[i].Range, Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position + _offset, direction, Attacks[i].Range, _enemysLayer);
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(Attacks[i].Damage);
                    print("hit");
                }
                else
                {
                    print("miss");
                }
                _timer = Attacks[i].FireRate;
            }
            else
            {
                _chargeAttack = true;
                _projectileSpeed = _minProjectileSpeed;
                _projectileDamage = Attacks[2].Damage;
                StartCoroutine(HoldRangeAttack());
            }
        }
    }
    private IEnumerator HoldRangeAttack()
    {
        while (_inputManager.holdRangeAttack() && _projectileSpeed < _maxProjectileSpeed && _projectileDamage < _maxProjectileDamage)
        {
            _projectileDamage += (_maxProjectileDamage - Attacks[2].Damage) / _chargeSpeed;
            _projectileSpeed += (_maxProjectileSpeed - _minProjectileSpeed) / _chargeSpeed;
            yield return null;
        }
        if (_projectileSpeed > _maxProjectileSpeed)
        {
            _projectileSpeed = _maxProjectileSpeed;
        }
        if (_projectileDamage > _maxProjectileDamage)
        {
            _projectileDamage = _maxProjectileDamage;
        }
        RangeAttack();
    }
    private void RangeAttack()
    {
        GameObject projectile = Instantiate(_projectile, new Vector2(transform.position.x, transform.position.y + _yOffset) + direction, Quaternion.Euler(0, 0, 0)) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * _projectileSpeed;
        projectile.GetComponent<Projectile>().damage = _projectileDamage;
        _chargeAttack = false;
        _timer = Attacks[2].FireRate;
    }
}
