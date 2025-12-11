using UnityEngine;

public interface ICharacterGravity
{
    public float g {get; set;}
    public void Initialize(CharacterController cc);
    public void Push(Vector3 power);
    public void PushX(float power);
    public void PushY(float power);
    public void PushZ(float power);
    public void Update();
    public void Exit(); 
}


