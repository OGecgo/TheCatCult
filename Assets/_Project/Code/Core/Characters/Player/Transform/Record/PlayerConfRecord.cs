using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerConfRecord
{
    private WalkConf _walkConf;
    private RunConf _runConf;
    private JumpConf _jumpConf;
    private FallConf _fallConf;

    public WalkConf walkConf { get{return _walkConf;} set{_walkConf = value;} }
    public RunConf runConf { get{return _runConf;} set{_runConf = value;} }
    public JumpConf jumpConf { get{return _jumpConf;} set{_jumpConf = value;} }
    public FallConf fallConf { get{return _fallConf;} set{_fallConf = value;} }

    public PlayerConfRecord
    (
        WalkConf walkConf,
        RunConf runConf,
        JumpConf jumpConf,
        FallConf fallConf
    )
    {
        _walkConf = walkConf;
        _runConf = runConf;
        _jumpConf = jumpConf;
        _fallConf = fallConf;
    }
}
