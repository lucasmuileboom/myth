using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    [SerializeField] private GameObject _attackBox;
    [SerializeField] private int _duration;

    public IEnumerator Attack()
    {
        for (int i = 0; i < _duration; i++)
        {
            _attackBox.transform.Translate(Vector3.left * 0.8f);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSecondsRealtime(3);
        for (int i = 0; i < _duration; i++)
        {
            _attackBox.transform.Translate(Vector3.right * 0.8f);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }	
}
