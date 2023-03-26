using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandHandler 
{
    void Initialize();

    event Action<Vector3> OnCommandMovement;

    event Action OnSpaceKeyCodePressed;

    void Dispose();
}
