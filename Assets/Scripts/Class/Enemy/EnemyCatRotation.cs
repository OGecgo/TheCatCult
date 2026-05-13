using UnityEngine;

public class EnemyCatRotation : IEnemyCatRotation
{

    private Vector3 _posTarget;
    public Vector3 posTarget { get { return _posTarget; } set { _posTarget = value; }}
    
    private IRotationControll contrl;
    private Transform posCatEnemy;

    
    public void Initialize(float sensitivity, float speedDirections, Transform transform)
    {
        contrl = new RotationControll();
        contrl.Initialize(sensitivity, Directions.Yaw, new Vector3(speedDirections, 0f, 0f), transform);
        posTarget = Vector3.zero;
        posCatEnemy = transform;
    }
    public void ChangeSensitivity(float newSensitivity)
    {
        contrl.sensitivity = newSensitivity;
    }

    public void Update()
    {
        if (posTarget != Vector3.zero){
            // Quaternion targerot = Quaternion.LookRotation((posTarget - posCatEnemy.position).normalized);
            // Quaternion q = new Quaternion(posTarget.x, posTarget.y, posTarget.z, 0f);
            // Quaternion lerp = Quaternion.Lerp(q, targerot, Time.deltaTime);
            // Vector3 temp = new Vector3(lerp.x, lerp.y, lerp.z);
            Vector3 rotate = posTarget - posCatEnemy.position;
            contrl.RotateTo(rotate);
            contrl.Update();
        }
    }


}
