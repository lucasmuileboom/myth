using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]private int _health;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            print("GameOver");
        }
    }
}
