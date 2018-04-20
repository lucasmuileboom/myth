using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _anim;
    private Vector3 _localScale;

	void Start ()
    {
        _anim = GetComponent<Animator>();
        _localScale = transform.localScale;
	}

    public void Flip(int direction)
    {
        transform.localScale = new Vector3(direction * _localScale.x, _localScale.y, _localScale.z);
    }

    public void SetSpeed(float speed)
    {
        _anim.speed = speed;
    }
}
