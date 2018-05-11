using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] int amountOfHealth;
    public Draw_hearths drawheart;

    void Start()
    {
        drawheart = GameObject.Find("Lives").GetComponent<Draw_hearths>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerHealth>().health += amountOfHealth;
            drawheart.AwakeHearth();
            Destroy(this.gameObject);
        }        
    }
}
