using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_shake : MonoBehaviour {

    public float shakeTimer;
    public float shakeAmout;

    private void Update()
    {//Calls the ShakeTimer function
        ShakeTimer();
        //Is for tests can be deleted
        if (Input.GetButtonDown("Fire1"))
        {
            ShakeCamera(1,0.2f);
        }
    }

    //Controls the camera shake and put's a timer on it
    public void ShakeTimer()
    {
        if (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmout;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
    }

    //Gives the perameters to the ShakeTimer function an is the call function, meaning this is the funcion that you call to shake the camera
    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmout = shakePwr;
        shakeTimer = shakeDur;
    }
}
