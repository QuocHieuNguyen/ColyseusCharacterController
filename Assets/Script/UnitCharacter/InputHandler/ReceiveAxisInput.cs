using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveAxisInput : ReceiveInput<AxisInput>
{
    private bool canBeInterrupted;

    public ReceiveAxisInput(bool canBeInterrupted, AxisInput target, Action<AxisInput> command) : base(target, command)
    {
        this.canBeInterrupted = canBeInterrupted;
    }
    
    public override bool ValidateInput()
    {
        return canBeInterrupted || Input.GetAxis(target.axisLabel) != 0;
    }

    public override void Execute()
    {
        target.response = Input.GetAxis(target.axisLabel);
        base.Execute();
    }


}

public class AxisInput
{
    public string axisLabel;
    public float response;
}
