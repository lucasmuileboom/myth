using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will manage the movement of the boss, which includes resting every now and then and being able to be slowed down
/// </summary>

public class BossMovement : MonoBehaviour
{
    [SerializeField]private float _speed;
    [SerializeField]private float _acceleration;
    [SerializeField]private float _minSpeed;
    [SerializeField]private float _maxSpeed;
    [SerializeField]private float _slowDown;
    [SerializeField]private float _rescaleValue;
    [SerializeField]private bool _active;
    [SerializeField]private bool _resting;
    [SerializeField]private int _restTimer;
    [SerializeField]private bool _clearEnemies;
    private bool _moving;
    private bool _started;
    private Vector2 _pos;
    private GameObject _player;
    private GameObject[] _platforms;



    // Use this for initialization
    void Start()
    {
        // rescale the variables to fit the properties
        _speed *= _rescaleValue;
        _acceleration *= _rescaleValue;
        _minSpeed *= _rescaleValue;
        _maxSpeed *= _rescaleValue;
        _slowDown *= _rescaleValue;

        _player = GameObject.FindGameObjectWithTag("Player");
        _platforms = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < _platforms.Length; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _platforms[i].GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _pos = transform.position;
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

    void LateUpdate()
    {
        if (!_moving && _speed == 0)
        {
            transform.position = _pos;
        }
    }

    void MoveBoss()
    {
        Debug.Log("Test1");
        if (_moving)
        {
            Debug.Log("Test2");
            if (!_resting && _speed < _minSpeed)
            {
                _speed = _minSpeed;
            }
            if (_speed < _maxSpeed)
            {
                _speed += _acceleration;
            }
            transform.Translate(Vector2.left * _speed);
        }
    }

    public void ActivateBoss()
    {
        _active = true;
    }

    public void ClearEnemies()
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
        Debug.Log("Starting Up!");
        _started = true;
        _resting = false;
        yield return new WaitForSeconds(1f);
        _moving = true;
        yield return new WaitForSecondsRealtime(10);
        StartCoroutine("Rest");
    }

    IEnumerator Rest()
    {
        Debug.Log("Resting");
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
        _speed -= _acceleration * 15;
    }
}
