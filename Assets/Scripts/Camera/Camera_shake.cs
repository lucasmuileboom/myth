using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_shake : MonoBehaviour {

    public float shakeTimer;
    public float shakeAmout;

    private void Update()
    {
        ShakeTimer();
        if (Input.GetButtonDown("Fire1"))
        {
            ShakeCamera(1,0.2f);
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
