using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour {

    public float shakeTimer;
    public float shakeAmout;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShakeCamera(0.1f, 1);
        }
        if(shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmout;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeAmout -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmout = shakePwr;
        shakeTimer = shakeDur;
    }
}
