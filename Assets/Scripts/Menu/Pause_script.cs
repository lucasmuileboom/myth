using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_script : MonoBehaviour {

    GameObject[] pauseObjects;

	void Start () {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPaused");
        hidePause();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseControl();
        }
    }

    public void PauseControl()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
           Time.timeScale = 1;
           hidePause();
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePause()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
}
