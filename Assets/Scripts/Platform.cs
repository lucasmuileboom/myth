using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private InputManager _inputManager;
    private GameObject _Player;
   // private float _playerHeight;
   [SerializeField]private float _platformHeight;
    private bool _goDown;

    void Start ()
    {
        _Player = GameObject.Find("Player");
        _inputManager = _Player.GetComponent<InputManager>();
        //_playerHeight = _Player.transform.lossyScale.y / 2;
        //_platformHeight = transform.lossyScale.y / 2;
    }
	void Update ()
    {
        if (_Player.transform.position.y > transform.position.y + _platformHeight && !_goDown)
        {
            if (_inputManager.down())
            {
                _goDown = true;
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = true;
                gameObject.layer = 8;
            }
        }
        else if (_Player.transform.position.y < transform.position.y)
        {
            _goDown = false;
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.layer = 0;
        }
    }
}
