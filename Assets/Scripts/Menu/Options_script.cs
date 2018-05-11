using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_script : MonoBehaviour {

    GameObject[] optionObjects;

	void Start () {
        optionObjects = GameObject.FindGameObjectsWithTag("ShowOnOptions");
        HideOption();
    }
	
    public void ShowOption()
    {
        foreach(GameObject g in optionObjects)
        {
            g.SetActive(true);
        }
    }

    public void HideOption()
    {
        foreach(GameObject g in optionObjects)
        {
            g.SetActive(false);
        }
    }
}
