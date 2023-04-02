using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour, IInputHandler
{
    private HashSet<IReceiveInput> registeredInput = new HashSet<IReceiveInput>();
    private void Update()
    {
        foreach (var receiveInput in registeredInput)
        {
            if (receiveInput.ValidateInput())
            {
                receiveInput.Execute();
            }
        }
    }

    public void RegisterInput(IReceiveInput receiveInput)
    {
        registeredInput.Add(receiveInput);
    }

    public void UnregisterInput(IReceiveInput receiveInput)
    {
        registeredInput.Remove(receiveInput);
    }
}



