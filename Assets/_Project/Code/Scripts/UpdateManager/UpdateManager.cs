
using UnityEngine;


// TODO: for now used if null. in the futture better will be think better way
// example with events. for avoid multiple if statments
public class UpdateManager : MonoBehaviour
{
    private IUpdatable[] updatable;
    private IStartable[] startable;
    private IAwakable[] awakable;

    public void Onestroy()
    {
        updatable = null;
        startable = null;
        awakable = null;
    }

    public void Awake()
    {
        updatable = this.GetComponentsInChildren<IUpdatable>();
        startable = this.GetComponentsInChildren<IStartable>();
        awakable  = this.GetComponentsInChildren<IAwakable> ();

        foreach (IAwakable a in awakable)
        {
            if ((UnityEngine.Object)a != null) a.ManualAwake();
        }
    }
    void Start()
    {
        foreach (IStartable s in startable)
        {
            if ((UnityEngine.Object)s != null) s.ManualStart();                
        }
    }
    void Update()
    {
        foreach (IUpdatable u in updatable)
        {
            if ((UnityEngine.Object)u != null) u.ManualUpdate();
        }
    }


}
