using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private HashSet<IReceiveInput> registeredInput = new HashSet<IReceiveInput>();
    public event Action<ReceivedInputResponse> OnInput; 
    private void Update()
    {
        foreach (var receiveInput in registeredInput)
        {
            if (receiveInput.ValidateInput())
            {
                OnInput?.Invoke(receiveInput.Response());
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



