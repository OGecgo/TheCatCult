using System;
using UnityEngine;

public interface IEnemyCatAction
{
    public event Action<IEnemyCatAction.ActionType> OnAction;
    
    public enum ActionType {FOLLOW_PLAYER, PATH_WALKS, RANDOM_WALKS, LOOK_AROUND, NONE}
}
