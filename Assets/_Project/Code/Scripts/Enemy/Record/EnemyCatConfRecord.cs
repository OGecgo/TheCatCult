using TMPro;
using UnityEngine;

public class EnemyCatConfRecord
{
    private WalkConf _walkConf;
    private RunConf _runConf;

    public WalkConf walkConf { get{return _walkConf;} set{_walkConf = value;} }
    public RunConf runConf { get{return _runConf;} set{_runConf = value;} }

    public EnemyCatConfRecord
    (
        WalkConf walkConf,
        RunConf runConf
    )
    {
        _walkConf = walkConf;
        _runConf = runConf;
    }
}
