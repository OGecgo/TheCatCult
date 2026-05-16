using UnityEngine;
using UnityEngine.InputSystem;

public class StateJump : IStateJump
{
    private float _heightJump;
    private Vector3 _moveTo;
    public float heightJump { get { return _heightJump; } set { _heightJump = value; } }
    public Vector3 moveTo {set { _moveTo = value;} }

    private ICharacterGravity characterGravity;

    // velocity is pointer to one obj. that needed for syncronize different state with movement
    public StateJump(ICharacterGravity cg, float heightJump)
    {
        this.characterGravity = cg;
        this.heightJump = heightJump;
    }



    public void Exit()
    {

    }
    public void Update()
    {
        // jump
        characterGravity.PushY(Mathf.Sqrt(heightJump * -2.0f * characterGravity.g));
    }
    public void Enter()
    {
        
    }
}