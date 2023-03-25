using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveSingleKeyCode : IReceiveInput
{
    private KeyCode _keyCode;
    private ReceivedSingleKeyCodeResponse _receivedSingleKeyCodeResponse;
    public ReceiveSingleKeyCode(KeyCode keyCode)
    {
        _keyCode = keyCode;
    }
    
    public bool ValidateInput()
    {
        return Input.GetKey(_keyCode);
    }

    public ReceivedInputResponse Response()
    {
        _receivedSingleKeyCodeResponse.KeyCode = _keyCode;
        return _receivedSingleKeyCodeResponse;
    }
}
