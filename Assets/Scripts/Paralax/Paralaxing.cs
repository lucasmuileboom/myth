using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxing : MonoBehaviour {

    private Transform _cameraTransform;
    private Transform[] _layers;
    private float _viewZone = 10;
    private int _leftIndex;
    private int _rightIndex;
    private float _lastCameraX;

    public float backgroundSize;
    public float paralaxSpeed;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraX = _cameraTransform.position.x;
        _layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            _layers[i] = transform.GetChild(i);

        _leftIndex = 0;
        _rightIndex = _layers.Length - 1;
    }

    private void Update()
    {
        float deltaX = _cameraTransform.position.x - _lastCameraX;
        transform.position += Vector3.right * (deltaX * paralaxSpeed);
        _lastCameraX = _cameraTransform.position.x;

        foreach (Transform f in _layers)
        {
            f.position = new Vector3(f.position.x, _cameraTransform.position.y, f.position.z);
        }

        if (_cameraTransform.position.x < (_layers[_leftIndex].transform.position.x + _viewZone))
            LeftScroll();

        if (_cameraTransform.position.x > (_layers[_rightIndex].transform.position.x - _viewZone))
            RightScroll();
    }

    private void LeftScroll()
    {
        int lastRight = _rightIndex;
        _layers[_rightIndex].position = Vector3.right * (_layers[_leftIndex].position.x - backgroundSize);
        _leftIndex = _rightIndex;
        _rightIndex--;
        if (_rightIndex < 0)
        {
            _rightIndex = _layers.Length - 1;
        }
    }

    private void RightScroll()
    {
        int lastLeft = _leftIndex;
        _layers[_leftIndex].position = Vector3.right * (_layers[_rightIndex].position.x + backgroundSize);
        _rightIndex = _leftIndex;
        _leftIndex++;
        if (_leftIndex == _layers.Length)
        {
            _leftIndex = 0;
        }
    }
}
