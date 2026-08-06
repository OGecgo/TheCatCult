
using UnityEngine;


// TODO: for now used if null. in the futture better will be think better way
// example with events. for avoid multiple if statments
public class EnemyCatUpdateManager : MonoBehaviour
{
    private IEnemyCat[] objs; // EnemyCat
     
    public void Awake()
    {
        objs = this.GetComponentsInChildren<EnemyCat>();
        foreach (IEnemyCat obj in objs)
        {
            if ((UnityEngine.Object)obj != null) obj.ManualAwake();
        }
    }
    void Start()
    {
        foreach (IEnemyCat obj in objs)
        {
            if ((UnityEngine.Object)obj != null) obj.ManualStart();                
        }
    }
    void Update()
    {
        foreach (IEnemyCat obj in objs)
        {
            if ((UnityEngine.Object)obj != null) obj.ManualUpdate();
        }
    }
}
