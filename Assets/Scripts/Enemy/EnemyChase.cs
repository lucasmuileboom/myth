using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is part of the enemy movement, when activated, it will make the enemy chase the player
/// </summary>

[RequireComponent(typeof(EnemyMovement))]

public class EnemyChase : MonoBehaviour
{
    [Header("GameObjects")]

    [Header("Components")]

    [Header("Ints and Floats")]
    
    [Header("Bools")]

    [Header("Vectors")]


    private EnemyMovement _movement;

    void Start()
    {
        _movement = GetComponent<EnemyMovement>();
    }



	// Update is called once per frame
	void Update () {
		
	}
}
