using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float move { get; private set; }
    public bool jump { get; private set; }
    public bool jumpLong { get; private set; }
    public bool jumpEnd { get; private set; }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButtonDown("Jump");
        //jumpLong = Input.GetButton("Jump");
        //jumpEnd = Input.GetButtonUp("Jump");
        
    }
}
