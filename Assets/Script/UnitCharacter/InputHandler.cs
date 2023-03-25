using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private HashSet<KeyCode> registeredKeyCode = new HashSet<KeyCode>();
    public event Action<KeyCode> OnGetKey; 
    private void Update()
    {
        foreach (var keyCode in registeredKeyCode)
        {
            if (Input.GetKey(keyCode))
            {
                OnGetKey?.Invoke(keyCode);
            }
        }
    }

    public void RegisterKeyCode(KeyCode keyCode)
    {
        registeredKeyCode.Add(keyCode);
    }

    public void UnregisterKeyCode(KeyCode keyCode)
    {
        registeredKeyCode.Remove(keyCode);
    }
}
