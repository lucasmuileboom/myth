using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BossTrigger")
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossMovement>().ActivateBoss();           
        }
    }
}
