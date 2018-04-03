using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject _target;

    [Header("Bools")]
    private bool _fleeing;

    [Header("Numbers")]
    private float _maxSpeed;
    private int _direction;

    public void GetData(float maxSpeed, bool fleeing, GameObject target)
    {
        _maxSpeed = maxSpeed;
        _fleeing = fleeing;
        _target = target;
    }

    void FixedUpdate()
    {
        if (_fleeing)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
    }

    public void ChaseObject(int targetDirection)
    {
        transform.position = new Vector2(transform.position.x + _maxSpeed * targetDirection * _direction * Time.deltaTime, transform.position.y);
        print(_maxSpeed);
    }
}
