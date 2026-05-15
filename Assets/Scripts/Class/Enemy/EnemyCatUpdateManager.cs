using UnityEngine;

public class EnemyCatUpdateManager : MonoBehaviour
{



    private IUpdateManagerMonoBehaviour[] objs; // EnemyCat
     
    public void Awake()
    {
        objs = this.GetComponentsInChildren<EnemyCat>();
        foreach (IUpdateManagerMonoBehaviour obj in objs)
        {
            obj.ManualAwake();
        }
    }
    void Start()
    {
        foreach (IUpdateManagerMonoBehaviour obj in objs)
        {
            obj.ManualStart();
        }
    }
    void Update()
    {
        foreach (IUpdateManagerMonoBehaviour obj in objs)
        {
            obj.ManualUpdate();
        }
    }
}
