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
    private ICommandHandler _commandHandler;
    private Vector3 _targetPosition;

    public void SetNetworkManager(NetworkManager networkManager)
    {
        _networkManager = networkManager;
    }
    public void SetInputHandler(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    public void SetCommandHandler(ICommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
        
        //_commandHandler = new CommandHandler(gameObject, _inputHandler);
        _commandHandler.Initialize();
        _commandHandler.OnSpaceKeyCodePressed += () => { Debug.Log("Atk"); };
        _commandHandler.OnCommandMovement += OnMovement;
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
        _targetPosition = movement;
        _moving = true;

    }
}
