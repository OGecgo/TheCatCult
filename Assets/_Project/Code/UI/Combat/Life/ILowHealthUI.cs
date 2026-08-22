using System;
using UnityEngine;

public interface ILowHealthUI
{
    public event Action OnIsLowHealth;
    public event Action OnIsNotLowhealth;
}
