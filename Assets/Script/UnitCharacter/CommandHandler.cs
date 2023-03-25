using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler
{
    private InputHandler _inputHandler;
    public event Action OnSpaceKeyCodePressed;
    public CommandHandler(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    public void Initialize()
    {
        _inputHandler.OnGetKey += OnKeyCodePressed;
        _inputHandler.RegisterKeyCode(KeyCode.Space);
    }

    private void OnKeyCodePressed(KeyCode keyCode)
    {
        if (keyCode == KeyCode.Space)
        {
            OnSpaceKeyCodePressed?.Invoke();
        }
    }

    public void Dispose()
    {
        _inputHandler.OnGetKey -= OnKeyCodePressed;
    }
}
