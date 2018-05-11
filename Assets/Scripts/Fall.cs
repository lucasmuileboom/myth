using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

    // Use this for initialization

    public loadSceneOnClick death;

    void Start()
    {
        death = GameObject.Find("_GM").GetComponent<loadSceneOnClick>();
    }


    // Update is called once per frame
   void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            death.LoadByIndex(2);
        }
    }
}
