using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{//moet down nog testen op controller
    public bool left()
    {
        if (Input.GetAxisRaw("Horizontal") < -0.25f || Input.GetKey(KeyCode.A))
        {
            return true;
        }
        return false;
    }
    public bool right()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.25f || Input.GetKey(KeyCode.D))
        {
            return true;
        }
        return false;
    }
    public bool down()
    {
        if (Input.GetAxisRaw("Vertical") > 0.25f || Input.GetKeyDown(KeyCode.S))
        {
            return true;
        }
        return false;
    }
    public bool jump()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }
        return false;
    }
    public bool holdjump()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.W))
        {
            return true;
        }
        return false;
    }
    public bool lightAttack()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.N))
        {
            return true;
        }
        return false;
    }
    public bool heavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.M))
        {
            return true;
        }
        return false;
    }
    public bool rangeAttack()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.K))
        {
            return true;
        }
        return false;
    }
    public bool holdRangeAttack()
    {
        if (Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.K))
        {
            return true;
        }
        return false;
    }
    public bool dodge()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            return true;
        }
        return false;
    }
}
