using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu_UI : MonoBehaviour {

    GameObject[] menuObjects;
    public bool main = true;

    void Start()
    {
        menuObjects = GameObject.FindGameObjectsWithTag("Main_Menu");
        showMain();
    }

    public void mainControl()
    {
        if (!main)
        {
            showMain();
        }
        else if (main)
        {
            hideMain();
        }
    }

    void showMain()
    {
        foreach (GameObject g in menuObjects)
        {
            g.SetActive(true);
        }
        main = true;
    }

    void hideMain()
    {
        foreach (GameObject g in menuObjects)
        {
            g.SetActive(false);
        }
        main = false;
    }
}
