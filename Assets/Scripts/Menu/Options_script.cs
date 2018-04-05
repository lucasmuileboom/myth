using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_script : MonoBehaviour {

    GameObject[] optionObjects;

	void Start () {
        optionObjects = GameObject.FindGameObjectsWithTag("ShowOnOptions");
        hideOption();
    }
	
    public void showOption()
    {
        foreach(GameObject g in optionObjects)
        {
            g.SetActive(true);
        }
    }

    public void hideOption()
    {
        foreach(GameObject g in optionObjects)
        {
            g.SetActive(false);
        }
    }
}
