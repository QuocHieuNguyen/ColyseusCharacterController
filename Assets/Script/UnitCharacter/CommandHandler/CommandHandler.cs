using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : ICommandHandler
{
    private const string HorizontalAxisLabel = "Horizontal";
    private const string VerticalAxisLabel = "Vertical";
    private GameObject target;
    private IInputHandler _inputHandler;
    private Vector3 movementValue;
    public event Action OnSpaceKeyCodePressed;

    public event Action<Vector3> OnCommandMovement; 
    public CommandHandler(GameObject target, IInputHandler inputHandler)
    {
        this.target = target;
        _inputHandler = inputHandler;
    }

    public void Initialize()
    {
        _inputHandler.RegisterInput(new ReceiveSingleKeyCode(KeyCode.Space, OnReceiveSingleKeyCode));
        _inputHandler.RegisterInput(new ReceiveAxisInput(true,GetAxisInput(HorizontalAxisLabel),  OnReceiveAxisInput));
        _inputHandler.RegisterInput(new ReceiveAxisInput(true,GetAxisInput(VerticalAxisLabel),  OnReceiveAxisInput));

    }
    
    private AxisInput GetAxisInput(string label)
    {
        return new AxisInput()
        {
            axisLabel = label
        };
    }
    private void OnReceiveSingleKeyCode(KeyCode keyCode)
    {
        if (keyCode== KeyCode.Space)
        {
            OnSpaceKeyCodePressed?.Invoke();
        }
    }

    private void OnReceiveAxisInput(AxisInput input)
    {
        if (input.axisLabel == HorizontalAxisLabel)
        {
            movementValue.x = input.response;
        }else if (input.axisLabel == VerticalAxisLabel)
        {
            movementValue.z = input.response;
        }
        OnCommandMovement?.Invoke(target.transform.position + movementValue);
    }


    public void Dispose()
    {

    }
}
