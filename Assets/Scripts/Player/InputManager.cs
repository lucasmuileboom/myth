using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{   //moet down nog testen op controller
    // moet escape controller fixe
    private Pause_script _Pause_script;
    public bool left()
    {
        return Input.GetAxisRaw("Horizontal") < -0.25f || Input.GetKey(KeyCode.A);
    }
    public bool right()
    {
        return Input.GetAxisRaw("Horizontal") > 0.25f || Input.GetKey(KeyCode.D);
    }
    public bool down()
    {
        return Input.GetAxisRaw("Vertical") > 0.25f || Input.GetKeyDown(KeyCode.S);
    }
    public bool jump()
    {
        return Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.W);
    }
    public bool holdjump()
    {
        return Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.W);
    }
    public bool lightAttack()
    {
        return Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.N);
    }
    public bool heavyAttack()
    {
        return Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.M);
    }
    public bool rangeAttack()
    {
        return Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.K);
    }
    public bool holdRangeAttack()
    {
        return Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.K);
    }
    public bool dodge()
    {
        return Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.LeftShift);
    }
    void Start()
    {
        _Pause_script = GameObject.Find("_GM").GetComponent<Pause_script>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//moet nog controler input
        {
            _Pause_script.PauseControl();
        }
       
    }
}
