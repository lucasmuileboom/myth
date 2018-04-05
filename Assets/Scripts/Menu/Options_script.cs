using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_script : MonoBehaviour {

    GameObject[] optionObjects;
    public bool option = false;

	void Start () {
        optionObjects = GameObject.FindGameObjectsWithTag("ShowOnOptions");
        hideOption();
    }
	
	public void optionControl () {
        if (!option)
        {
            showOption();
        }
        else if (option)
        {
            hideOption();
        }
	}

    void showOption()
    {
        foreach(GameObject g in optionObjects)
        {
            g.SetActive(true);
        }
        option = true;
    }

    void hideOption()
    {
        foreach(GameObject g in optionObjects)
        {
            g.SetActive(false);
        }
        option = false;
    }
}
