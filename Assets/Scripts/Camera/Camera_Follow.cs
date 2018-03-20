using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 offset;
    [SerializeField]
    private Vector3 extra;
    private bool reach = false;
    private bool low = false;
    private bool bossCamera = false;
    private Camera mainCamera;
    public float shakeTimer;
    public float shakeAmout;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        offset = transform.position - player.transform.position + extra;
    }

    void Update()
    {
        Follow();
        if (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmout;
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Max_Height")
        {
            reach = true;
        }
        if (other.gameObject.tag == "Min_Height")
        {
            low = true;
        }
        if(other.gameObject.tag == "BossRoom")
        {
            bossCamera = true;
        }
    }

void Follow()
    {
        if (!reach && !bossCamera)
        {
            this.transform.position = player.transform.position + offset;
        }
        else if (reach && !bossCamera)
        {
            this.transform.position = new Vector3(player.transform.position.x + extra.x, this.transform.position.y, this.transform.position.z);
            if (this.transform.position.y > player.transform.position.y)
            {
                reach = false;
            }
        }
        else if (low && !bossCamera)
        {
            this.transform.position = new Vector3(player.transform.position.x + extra.x, this.transform.position.y, this.transform.position.z);
            if(this.transform.position.y < player.transform.position.y)
            {
                low = false;
            }
        }
        if (bossCamera)
        {
            mainCamera.enabled = true;
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 10f, 0.2f);
        }
    }
    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmout = shakePwr;
        shakeTimer = shakeDur;
    }
}
