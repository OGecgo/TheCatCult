using UnityEngine;

public class JumpControl : IJumpControl
{
    private bool _doJump;

    private JumpConf config;
    private ICharacterGravity characterGravity;
    public bool doJump {set{_doJump = value;}}

    public JumpControl(ICharacterGravity cg, JumpConf jumpConf)
    {
        config = jumpConf;
        characterGravity = cg;
        _doJump = false;
    }

    public void ManualUpdate()
    {
        // jump
        if (_doJump)
            characterGravity.PushUp(Mathf.Sqrt(config.JumpHeight * -2.0f * characterGravity.g));
        _doJump = false;
    }

}