using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_UI : MonoBehaviour {

    GameObject[] menuObjects;

    void Start()
    {
        menuObjects = GameObject.FindGameObjectsWithTag("Main_Menu");
        showMain();
    }

    public void showMain()
    {
        foreach (GameObject g in menuObjects)
        {
            g.SetActive(true);
        }
    }

    public void hideMain()
    {
        foreach (GameObject g in menuObjects)
        {
            g.SetActive(false);
        }
    }
}
