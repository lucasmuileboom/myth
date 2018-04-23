using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private InputManager _inputManager;
    private GameObject _Player;
   [SerializeField]private float _platformHeight;
    private bool _goDown;

    void Start ()
    {
        _Player = GameObject.Find("Player");
        _inputManager = _Player.GetComponent<InputManager>();
    }
	void Update ()
    {
        if (_Player.transform.position.y > transform.position.y + _platformHeight && !_goDown)
        {//
            if (_inputManager.down() && _Player.transform.position.y < transform.position.y + _platformHeight + 0.5f)
            {
                _goDown = true;
                //Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
                print("down");
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                //Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>(),false);
                GetComponent<BoxCollider2D>().enabled = true;
                gameObject.layer = 8;
            }
        }
        else if (_Player.transform.position.y < transform.position.y)
        {
            _goDown = false;
            //Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider2D>(), GetComponent<Collider2D>());
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.layer = 0;
        }
    }
}
