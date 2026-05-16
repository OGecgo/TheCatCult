using UnityEngine;

public class CharacterGravity: ICharacterGravity
{
    public float g { get { return conf.g; } set { conf.g = value; } }
    public bool IsGrounded { get { return controller.isGrounded; } }

    private CharacterGravityConf conf;    
    private CharacterController controller;
    private Vector3 verticalVelocity;



    public CharacterGravity(CharacterController cc)
    {
        this.controller = cc;
        verticalVelocity = new Vector3(0f, 0f, 0f);
        conf = Resources.Load<CharacterGravityConf>("Configuration/CharacterGravityConf");
    }


    public void Push(Vector3 power)
    {
        verticalVelocity = power;
    }

    public void PushY(float power)
    {
        verticalVelocity.y = power;
    }    
    public void PushX(float power)
    {
        verticalVelocity.x = power;
    }    
    public void PushZ(float power)
    {
        verticalVelocity.z = power;
    }



    public void Update()
    {
        verticalVelocity.y += g * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = 0f;
        }
    }

    public void Exit()
    {

    }
}
