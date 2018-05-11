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
    private bool _deployed, _done;

    void Start()
    {
        Randomize();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !_deployed)
        {
            _deployed = true;
            GameObject.Find("Main Camera").GetComponent<Camera_Follow>().GetTarget(this.gameObject, new Vector3(0f, 0f, 0f));
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
            if (_amount <= 0 && !_done)
            {
                _done = true;
                print("Done");
                GameObject.Find("Main Camera").GetComponent<Camera_Follow>().GetTarget(GameObject.Find("Player"), new Vector3(0f, 0f, 0f));
            }
        }   
    }

    void Randomize()
    {
        _randDir = Random.Range(-1, 2);
    }
}
