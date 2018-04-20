using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;

    private Timer _timer;

    [SerializeField]
    private float _time;
    [SerializeField]
    private float _amount;
    private float _offset = 11;
    private int _randDir;
    [SerializeField]
    private bool _deployed = false;

    void Start()
    {
        Randomize();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && !_deployed)
        {
            _deployed = true;
            _timer = new Timer(_time);
        }
    }

    void FixedUpdate()
    {
        if (_randDir == 0)
            Randomize();
        if (_timer != null)
        {
            if (_amount > 0)
            {
                if (_timer.Done())
                {
                    Randomize();
                    while (_randDir == 0)
                        Randomize();
                    _timer = new Timer(_time);
                    print(_amount + " left");
                    _amount--;
                    Instantiate(_enemy, new Vector2(transform.position.x + _offset * _randDir, transform.position.y), Quaternion.identity);
                }
            }
            if (_amount <= 0)
            {
                print("Done");
            }
        }   
    }

    void Randomize()
    {
        _randDir = Random.Range(-1, 2);
    }
}
