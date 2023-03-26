using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class NetworkCommandHandler : ICommandHandler
{
    private const string HorizontalAxisLabel = "Horizontal";
    private const string VerticalAxisLabel = "Vertical";
    private GameObject target;
    private InputHandler _inputHandler;
    private NetworkManager _networkManager;
    
    private Vector3 movementValue;
    public event Action<Vector3> OnCommandMovement;
    public event Action OnSpaceKeyCodePressed;
    
    public NetworkCommandHandler(GameObject target, InputHandler inputHandler, NetworkManager networkManager)
    {
        this.target = target;
        _inputHandler = inputHandler;
        _networkManager = networkManager;
    }

    public void Initialize()
    {
        _inputHandler.OnInput += OnKeyCodePressed;
        _inputHandler.RegisterInput(new ReceiveSingleKeyCode(KeyCode.Space));
        _inputHandler.RegisterInput(new ReceiveAxisInput(HorizontalAxisLabel, true));
        _inputHandler.RegisterInput(new ReceiveAxisInput(VerticalAxisLabel, true));
        
        _networkManager.GameRoom.State.OnChange += OnStateChangeHandler;
    }

    private void OnStateChangeHandler(List<DataChange> onChangeEventHandler)
    {
        var player = _networkManager.GameRoom.State.players[_networkManager.GameRoom.SessionId];
        OnCommandMovement?.Invoke(new Vector3(player.x, player.y, player.z));
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
        if (movementValue != Vector3.zero)
        {
            Vector3 updatedPosition = target.transform.position + movementValue;
            _networkManager.SendPlayerPosition(updatedPosition);
        }

    }
    public void Dispose()
    {
        _inputHandler.OnInput -= OnKeyCodePressed;
        _networkManager.GameRoom.State.OnChange -= OnStateChangeHandler;
    }
}
