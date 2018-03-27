using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will manage the movement of the boss, which includes resting every now and then, and being able to be slowed down
/// </summary>

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _slowDown;
    [SerializeField] private float _rescaleValue;
    [SerializeField] private int _attackTime;
    [SerializeField] private int _attackTimeMax;
    [SerializeField] private bool _active;
    [SerializeField] private bool _resting;
    [SerializeField] private bool _clearEnemies;
    private bool _moving;
    private bool _started;
    private GameObject[] _platforms;
    private BossAttack _bossAttack;

    // Use this for initialization
    void Start()
    {
        // rescale the variables to fit the properties
        _speed *= _rescaleValue;
        _acceleration *= _rescaleValue;
        _minSpeed *= _rescaleValue;
        _maxSpeed *= _rescaleValue;
        _slowDown *= _rescaleValue;
        _platforms = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < _platforms.Length; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _platforms[i].GetComponent<Collider2D>());
        }
        _bossAttack = GetComponent<BossAttack>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_active)
        {
            if (_clearEnemies)
            {
                ClearEnemies();
            }
            MoveBoss();
            if (!_started)
            {
                StartCoroutine("Startup");
            }
        }
    }

    void MoveBoss()
    {
        if (_moving)
        {
            if (!_resting)
            {
                _attackTime++;
                if (_speed < _minSpeed)
                {
                    _speed = _minSpeed;
                }
            }
            if (_speed < _maxSpeed)
            {
                _speed += _acceleration;
            }
            transform.position -= new Vector3(_speed, 0f);
        }
        if (_attackTime >= _attackTimeMax)
        {
            _bossAttack.StartCoroutine(_bossAttack.Attack());
            _attackTime = 0;
        }
    }

    public void ActivateBoss()
    {
        _active = true;
    }

    void ClearEnemies()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < a.Length; i++)
        {
            Destroy(a[i]);
        }
        _clearEnemies = false;
    }

    IEnumerator Startup()
    {
        _started = true;
        _resting = false;
        yield return new WaitForSeconds(1f);
        _moving = true;
        yield return new WaitForSecondsRealtime(10);
        StartCoroutine("Rest");
    }

    IEnumerator Rest()
    {
        _resting = true;
        _moving = false;
        while (_speed > 0)
        {
            _speed -= _acceleration;
            yield return new WaitForFixedUpdate();
        }
        if (_speed <= 0)
        {
            _speed = 0;
        }
        yield return new WaitForSecondsRealtime(3);
        StartCoroutine("Startup");

    }

    public void SlowDown()
    {
        if (_speed > 0 && !_moving)
        {
            _speed -= _acceleration * 50;
        }
    }
}
