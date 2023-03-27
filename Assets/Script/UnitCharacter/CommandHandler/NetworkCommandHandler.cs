using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class NetworkCommandHandler : ICommandHandler
{
    private const string HorizontalAxisLabel = "Horizontal";
    private const string VerticalAxisLabel = "Vertical";
    private string playerSessionID;
    private GameObject target;
    private InputHandler _inputHandler;
    private NetworkManager _networkManager;
    
    private Vector3 movementValue;
    public event Action<Vector3> OnCommandMovement;
    public event Action OnSpaceKeyCodePressed;
    
    public NetworkCommandHandler(string playerSessionID,GameObject target, InputHandler inputHandler, NetworkManager networkManager)
    {
        this.playerSessionID = playerSessionID;
        this.target = target;
        _inputHandler = inputHandler;
        _networkManager = networkManager;
    }

    public void Initialize()
    {
        //_inputHandler.OnInput += OnKeyCodePressed;
        _inputHandler.RegisterInput(new ReceiveSingleKeyCode(KeyCode.Space, OnReceiveSingleKeyCode));
        _inputHandler.RegisterInput(new ReceiveAxisInput(true,GetAxisInput(HorizontalAxisLabel),  OnReceiveAxisInput));
        _inputHandler.RegisterInput(new ReceiveAxisInput(true,GetAxisInput(VerticalAxisLabel),  OnReceiveAxisInput));
        
        _networkManager.GameRoom.State.OnChange += OnStateChangeHandler;
    }

    private AxisInput GetAxisInput(string label)
    {
        return new AxisInput()
        {
            axisLabel = label
        };
    }
    private void OnStateChangeHandler(List<DataChange> onChangeEventHandler)
    {
        var player = _networkManager.GameRoom.State.players[playerSessionID];
        OnCommandMovement?.Invoke(new Vector3(player.x, player.y, player.z));
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
        if (movementValue != Vector3.zero)
        {
            Vector3 updatedPosition = target.transform.position + movementValue;
            _networkManager.SendPlayerPosition(updatedPosition);
        }
    }
    

    public void Dispose()
    {
        _networkManager.GameRoom.State.OnChange -= OnStateChangeHandler;
    }
}
