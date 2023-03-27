using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveSingleKeyCode : ReceiveInput<KeyCode>
{
    public ReceiveSingleKeyCode(KeyCode target, Action<KeyCode> command) : base(target, command)
    {
    }
    
    public override bool ValidateInput()
    {
        return Input.GetKey(target);
    }
    

}
