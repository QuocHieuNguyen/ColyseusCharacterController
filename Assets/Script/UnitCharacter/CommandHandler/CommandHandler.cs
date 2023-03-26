using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : ICommandHandler
{
    private const string HorizontalAxisLabel = "Horizontal";
    private const string VerticalAxisLabel = "Vertical";
    private GameObject target;
    private InputHandler _inputHandler;
    private Vector3 movementValue;
    public event Action OnSpaceKeyCodePressed;

    public event Action<Vector3> OnCommandMovement; 
    public CommandHandler(GameObject target, InputHandler inputHandler)
    {
        this.target = target;
        _inputHandler = inputHandler;
    }

    public void Initialize()
    {
        _inputHandler.OnInput += OnKeyCodePressed;
        _inputHandler.RegisterInput(new ReceiveSingleKeyCode(KeyCode.Space));
        _inputHandler.RegisterInput(new ReceiveAxisInput(HorizontalAxisLabel, true));
        _inputHandler.RegisterInput(new ReceiveAxisInput(VerticalAxisLabel, true));

    }

    private void OnKeyCodePressed(ReceivedInputResponse receivedInputResponse)
    {
        if (receivedInputResponse is ReceivedSingleKeyCodeResponse singleKeyCodeResponse )
        {
            if (singleKeyCodeResponse.KeyCode == KeyCode.Space)
            {
                OnSpaceKeyCodePressed?.Invoke();
            }
           
        }else if (receivedInputResponse is ReceivedAxisResponse receivedAxisResponse)
        {

            if (receivedAxisResponse.ResponseLabel == HorizontalAxisLabel)
            {
                movementValue.x = receivedAxisResponse.InputValue;
            }else if (receivedAxisResponse.ResponseLabel == VerticalAxisLabel)
            {
                movementValue.z = receivedAxisResponse.InputValue;
            }
            
            
        }
        OnCommandMovement?.Invoke(target.transform.position + movementValue);
    }

    public void Dispose()
    {
        _inputHandler.OnInput -= OnKeyCodePressed;
    }
}
