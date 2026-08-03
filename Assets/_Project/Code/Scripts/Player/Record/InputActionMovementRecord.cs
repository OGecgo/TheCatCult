using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionMovementRecord
{
    private InputActionReference _move;
    private InputActionReference _run; 
    private InputActionReference _jump;

    public InputActionReference move { get{return _move;} set{_move = value;} }
    public InputActionReference run { get{return _run;} set{_run = value;} }
    public InputActionReference jump { get{return _jump;} set{_jump = value;} }

    public InputActionMovementRecord
    (
        InputActionReference move, 
        InputActionReference run, 
        InputActionReference jump
    )
    {
        _move = move;
        _run = run;
        _jump = jump;
    }
}
