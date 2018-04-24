using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _anim;
    private Vector3 _localScale;
    private EnemyCheckSurroundings _checkSur;

	void Start()
    {
        _anim = GetComponent<Animator>();
        _checkSur = GetComponent<EnemyCheckSurroundings>();
        _localScale = transform.localScale;
        _anim.Play("enemy-run");
    }

    void FixedUpdate()
    {
        _anim.SetBool("grounded", _checkSur.IsGrounded());
    }

    public void Flip(int direction)
    {
        if (direction != 0)
            transform.localScale = new Vector3(direction * _localScale.x, _localScale.y, _localScale.z);
    }

    public void SetSpeed(float speed)
    {
        _anim.speed = speed;
    }

    public void Attacking(bool attacking)
    {
        _anim.SetBool("attacking", attacking);
    }
}
