using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {

    private GameObject[] _other;
	// Use this for initialization
	void Start () {
        _other = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < _other.Length; i++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _other[i].GetComponent<Collider2D>());
        }
    }
}
