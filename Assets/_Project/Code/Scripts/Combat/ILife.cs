using System;
using UnityEngine;
using UnityEngine.Timeline;

public interface ILife
{   
    public event Action OnDie;
    public void Attack(int power);
}
