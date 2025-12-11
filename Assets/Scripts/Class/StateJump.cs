using UnityEngine;
using UnityEngine.InputSystem;

public class StateJump : IStateJump
{
    private ICharacterGravity characterGravity;
    private float _heightJump;
    public float heightJump { get { return _heightJump; } set { _heightJump = value; } }



    public void Initialize(ICharacterGravity cg, float heightJump)
    {
        characterGravity = cg;
        this.heightJump = heightJump;
    }



    public void Exit()
    {

    }
    public void Update()
    {

        characterGravity.PushY(Mathf.Sqrt(heightJump * -2.0f * characterGravity.g));
    }
    public void Enter()
    {
        
    }
}