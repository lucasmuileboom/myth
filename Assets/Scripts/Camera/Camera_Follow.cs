﻿using System.Collections;
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
    public float shakeTimer;
    public float shakeAmout;

    void Start()
    {
        _boss = GameObject.FindWithTag("Boss");
        _mainCamera = gameObject.GetComponent<Camera>();
        _offset = transform.position - _player.transform.position + _extra;
    }

    void Update()
    {
        Follow();
        ShakeTimer();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Max_Height")
        {
            _reach = true;
        }
        if (other.gameObject.tag == "Min_Height")
        {
            _low = true;
        }
        if(other.gameObject.tag == "BossRoom")
        {
            _bossCamera = true;
        }
    }

    void Follow()
    {
        if (!_reach && !_bossCamera)
        {
            this.transform.position = _player.transform.position + _offset;
        }
        else if (_reach && !_bossCamera)
        {
            this.transform.position = new Vector3(_player.transform.position.x + _extra.x, this.transform.position.y, this.transform.position.z);
            if (this.transform.position.y > _player.transform.position.y)
            {
                _reach = false;
            }
        }
        else if (_low && !_bossCamera)
        {
            this.transform.position = new Vector3(_player.transform.position.x + _extra.x, this.transform.position.y, this.transform.position.z);
            if(this.transform.position.y < _player.transform.position.y)
            {
                _low = false;
            }
        }
        if (_bossCamera)
        {
            this.transform.position = new Vector3(_boss.transform.position.x, this.transform.position.y, this.transform.position.z);
            _mainCamera.enabled = true;
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, 10f, 0.2f);
        }
    }

    public void ShakeTimer()
    {
        if (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmout;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmout = shakePwr;
        shakeTimer = shakeDur;
    }
}
