using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //moet de controler nog toevoegen
    public bool left()
    {
        return Input.GetKey(KeyCode.A);
    }
    public bool right()
    {
        return Input.GetKey(KeyCode.D);
    }
    public bool jump()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
    public bool holdjump()
    {
        return Input.GetKey(KeyCode.W);
    }
    public bool lightAttack()
    {
        return Input.GetKeyDown(KeyCode.N);
    }
    public bool heavyAttack()
    {
        return Input.GetKeyDown(KeyCode.M);
    }
    public bool rangeAttack()
    {
        return Input.GetKeyDown(KeyCode.K);
    }
    public bool holdRangeAttack()
    {
        return Input.GetKey(KeyCode.K);
    }
}
