using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using UnityEngine;

public class NetworkCommandHandlerNonLocal : ICommandHandler
{
    private string playerSessionID;
    private NetworkManager _networkManager;
    
    private Vector3 movementValue;
    public event Action<Vector3> OnCommandMovement;
    public event Action OnSpaceKeyCodePressed;
    
    public NetworkCommandHandlerNonLocal(string playerSessionID, NetworkManager networkManager)
    {
        this.playerSessionID = playerSessionID;
        _networkManager = networkManager;
    }
    public void Initialize()
    {
        _networkManager.GameRoom.State.OnChange += OnStateChangeHandler;
    }
    private void OnStateChangeHandler(List<DataChange> onChangeEventHandler)
    {
        var player = _networkManager.GameRoom.State.players[playerSessionID];
        OnCommandMovement?.Invoke(new Vector3(player.x, player.y, player.z));
    }

    public void Dispose()
    {
        _networkManager.GameRoom.State.OnChange -= OnStateChangeHandler;
    }
}
