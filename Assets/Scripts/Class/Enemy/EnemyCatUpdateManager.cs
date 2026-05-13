using UnityEngine;

public class EnemyCatUpdateManager : MonoBehaviour
{



    private IUpdateManagerObject[] objs; // EnemyCat
     
    public void Awake()
    {
        objs = this.GetComponentsInChildren<EnemyCat>();
        foreach (IUpdateManagerObject obj in objs)
        {
            obj.ManualAwake();
        }
    }
    void Start()
    {
        foreach (IUpdateManagerObject obj in objs)
        {
            obj.ManualStart();
        }
    }
    void Update()
    {
        foreach (IUpdateManagerObject obj in objs)
        {
            obj.ManualUpdate();
        }
    }
}
