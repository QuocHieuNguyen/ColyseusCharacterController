using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveInput<T> : IReceiveInput
{
    protected T target;
    protected Action<T> command;
    public ReceiveInput(T target, Action<T> command)
    {
        this.target = target;
        this.command = command;
    }
    
    public virtual bool ValidateInput()
    {
        return true;
    }

    public virtual void Execute()
    {
        command?.Invoke(target);
    }
}
