using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator _Animator;
    [SerializeField]private int _health;
    private Draw_hearths _drawHeart;
    private loadSceneOnClick _death;
    private int _maxhealth;
    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _maxhealth = _health;
	_drawHeart = GameObject.Find("Lives").GetComponent<Draw_hearths>();
        _death = GameObject.Find("_GM").GetComponent<loadSceneOnClick>();
    }
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health >_maxhealth)
            {
                _health = _maxhealth;
            }
        }
    }
    void Update()
    {
        _Animator.SetBool("hit", false);
    }
    public void TakeDamage(int damage)
    {
        _Animator.SetBool("hit", true);
        _health -= damage;
	_drawHeart.HideHearth();
        if (_health <= 0)
        {
            print("GameOver");
            _Animator.SetBool("death", true);
            //death
	    StartCoroutine(Death());
        }
    }
    private IEnumerator Death()
    {
	yield return new WaitForSeconds(0.5f);
	_death.LoadByIndex(2);
    }
}