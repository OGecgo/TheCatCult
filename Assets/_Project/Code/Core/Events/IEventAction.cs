using System;
using UnityEngine;

public interface IEventAction
{
    // time to end event
    public event Action<float> OnEventTriggered;
}
