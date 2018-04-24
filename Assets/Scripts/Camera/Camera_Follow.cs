using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private GameObject _boss;
    private Vector3 _offset;
    [SerializeField]
    private Vector3 _extra;
    private bool _reach = false;
    private bool _low = false;
    private bool _bossCamera = false;
    private Camera _mainCamera;

    void Start()
    {
        _boss = GameObject.FindWithTag("Boss");
        _mainCamera = gameObject.GetComponent<Camera>();
        _offset = transform.position - _player.transform.position + _extra;
    }

    void Update()
    {
        Follow();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Checks the collision triggers and changes the bools to true or false
        if (other.gameObject.tag == "Max_Height")
        {
            _reach = true;
        }
        if (other.gameObject.tag == "Min_Height")
        {
            _low = true;
        }
        if (other.gameObject.tag == "BossTrigger")
        {
            _bossCamera = true;
        }
        if (other.gameObject.tag == "player" && _boss.GetComponent<EnemyHealth>().GetHealth() == 0f)
        {
            _bossCamera = false;
        }
    }

    void Follow()
    {//Decides what the camera is following and keeps the camera within the levels boundries
        if (!_reach && !_bossCamera)
        {
            this.transform.position = _player.transform.position + _offset;
            _mainCamera.enabled = true;
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, 18f, 0.2f);
        }
        else if (_reach && !_bossCamera)
        {
            this.transform.position = new Vector3(_player.transform.position.x + _extra.x, this.transform.position.y, this.transform.position.z);
            _mainCamera.enabled = true;
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, 18f, 0.2f);
            if (this.transform.position.y > _player.transform.position.y)
            {
                _reach = false;
            }
        }
        else if (_low && !_bossCamera)
        {
            this.transform.position = new Vector3(_player.transform.position.x + _extra.x, this.transform.position.y, this.transform.position.z);
            _mainCamera.enabled = true;
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, 18f, 0.2f);
            if (this.transform.position.y < _player.transform.position.y)
            {
                _low = false;
            }
        }
        if (_bossCamera)
        {
            this.transform.position = new Vector3(_boss.transform.position.x - _extra.x, _boss.transform.position.y, this.transform.position.z);
            _mainCamera.enabled = true;
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, 30f, 0.2f);
        }
    }

    public void GetTarget(GameObject target, Vector3 pos)
    {
        _player = target;
        _extra = pos;
    }
}
