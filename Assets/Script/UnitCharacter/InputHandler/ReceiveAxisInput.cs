using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveAxisInput : IReceiveInput
{
    private string axisLabel;
    private bool canBeInterrupted;
    private ReceivedAxisResponse _response;

    public ReceiveAxisInput(string axisLabel, bool canBeInterrupted)
    {
        this.axisLabel = axisLabel;
        this.canBeInterrupted = canBeInterrupted;
        this._response = new ReceivedAxisResponse()
        {
            ResponseLabel = axisLabel
        };
    }

    public bool ValidateInput()
    {
        return canBeInterrupted || Input.GetAxis(axisLabel) != 0;
    }

    public ReceivedInputResponse Response()
    {
        this._response.InputValue = Input.GetAxis(axisLabel);
        return this._response;
    }
}
