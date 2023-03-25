using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private bool _moving;
    [SerializeField]
    private NetworkManager _networkManager;

    [SerializeField] private InputHandler _inputHandler;
    private CommandHandler _commandHandler;
    private Vector3 _targetPosition;

    private async void Start()
    {
        _commandHandler = new CommandHandler(_inputHandler);
        _commandHandler.Initialize();
        _commandHandler.OnSpaceKeyCodePressed += () =>
        {
            Debug.Log("Atk");
        };
        _commandHandler.OnCommandMovement += OnMovement;
        await _networkManager.JoinOrCreateGame();
        _networkManager.GameRoom.OnMessage<string>("welcomeMessage", message =>
        {
            Debug.Log(message);
        });
        // Set player's new position after synchronized the mouse click's position with the Colyseus server. 
        _networkManager.GameRoom.State.OnChange += (changes) =>
        {
            var player = _networkManager.GameRoom.State.players[_networkManager.GameRoom.SessionId];
            Debug.Log($"{player.x} and {player.y} and {player.z}");
            _targetPosition = new Vector3(player.x, player.y, player.z);
            _moving = true;
        };
        _networkManager.GameRoom.State.players.OnAdd += (key, player) =>
        {
            Debug.Log($"Player {key} has joined the Game!");
        };

    }
    private void Update()
    {
        if (_moving && (Vector3)transform.position != _targetPosition)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
        }
        else
        {
            _moving = false;
        }
    }

    void OnMovement(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            Vector3 updatedPosition = transform.position+movement;
            _networkManager.SendPlayerPosition(updatedPosition);
        }

    }
}
