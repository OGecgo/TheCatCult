using UnityEngine;

public class EnemyCatUpdateManager : MonoBehaviour
{
    [Header("Parrent Obj with child")]
    public GameObject parentObj;
    [Header("Enemy Cat Config FOV Detection")]
    public GameObject target; // player
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    [Header("Enemy Cat Config Movement")]
    public float speedMove;
    public float heightJump;
    public float sensitivityRotat;



    private IUpdateManagerObjectEnemyCat[] objs; // also GameObjects EnemyCat
     
    public void Awake()
    {
        objs = parentObj.GetComponentsInChildren<EnemyCat>();
        foreach (IUpdateManagerObjectEnemyCat obj in objs)
        {
            obj.InitializeFOVDirection(target, targetMask, obstructionMask, radius, angle);
            obj.InitializeMovement(heightJump, speedMove);
            obj.InitializeRotation(target, sensitivityRotat);
            obj.ManualAwake();
        }
    }
    void Start()
    {
        foreach (IUpdateManagerObjectEnemyCat obj in objs)
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
