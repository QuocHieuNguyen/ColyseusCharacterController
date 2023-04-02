using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    public void RegisterInput(IReceiveInput receiveInput);

    public void UnregisterInput(IReceiveInput receiveInput);
}
