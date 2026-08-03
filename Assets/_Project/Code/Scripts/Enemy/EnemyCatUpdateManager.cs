using UnityEngine;

public class EnemyCatUpdateManager : MonoBehaviour
{
    private IEnemyCat[] objs; // EnemyCat
     
    public void Awake()
    {
        objs = this.GetComponentsInChildren<EnemyCat>();
        foreach (IEnemyCat obj in objs)
        {
            obj.ManualAwake();
        }
    }
    void Start()
    {
        foreach (IEnemyCat obj in objs)
        {
            obj.ManualStart();
        }
    }
    void Update()
    {
        foreach (IEnemyCat obj in objs)
        {
            obj.ManualUpdate();
        }
    }
}
