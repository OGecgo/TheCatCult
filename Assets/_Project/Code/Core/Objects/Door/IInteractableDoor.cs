using System;
using UnityEngine;

public interface IInteractableDoor
{
    public void Interact();
    public event Action OnInteracted;
}
