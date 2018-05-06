using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_hearths : MonoBehaviour {

    private Transform[] _Lives;
    private int _lastIndex;

    void Start() {
        _Lives = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _Lives[i] = transform.GetChild(i);
        }
        _lastIndex = _Lives.Length - 1;
    }

    void AwakeHearth()
    {
        _lastIndex++;
        _Lives[_lastIndex].gameObject.SetActive(true);
    }

    void HideHearth()
    {
        _Lives[_lastIndex].gameObject.SetActive(false);
        _lastIndex--;
    }
}
