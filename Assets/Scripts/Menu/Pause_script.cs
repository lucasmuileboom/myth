using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_script : MonoBehaviour {

    GameObject[] pauseObjects;

	void Start () {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPaused");
        HidePause();
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
            ShowPaused();
        }
        else if (Time.timeScale <= 0)
        {
            Time.timeScale = 1;
            HidePause();
        }
    }

    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void HidePause()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
}
