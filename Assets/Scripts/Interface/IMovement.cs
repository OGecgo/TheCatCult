using UnityEngine;

public interface IMovement
{
    // for corent movement you should update jumpTrue and moveTo every frame
    public bool jumpTrue {get; set;}
    public Vector3 moveTo {get; set;}

    public void Initialize(CharacterController cc, float heightJump, float speedMove);
    public void UpdateHeightJump(float heightJump);
    public void UpdateSpeedMove(float speedMove);
    public void Update();


}
