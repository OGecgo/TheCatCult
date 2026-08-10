using UnityEngine;

public class RotationControl : IRotationControl
{
    private RotationConf config;
    private Transform transform;

    public RotationControl(RotationConf rotationConf, Transform objTransform)
    {
        config = rotationConf;
        transform = objTransform;
    }


    public void UpdateRotateTo(Vector3 target) 
    { 
        Vector3 tempTarget = target - transform.position;
        // apply directoin
        tempTarget = Vector3.Scale(tempTarget, new Vector3(config.xDirection, config.yDirection, config.zDirection)); 
        Quaternion lookRotation = Quaternion.LookRotation(tempTarget);  
        Quaternion temp_q = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * config.sensitivity);
        // Clamp final euler angles
        tempTarget = temp_q.eulerAngles;
        tempTarget = TestDomain(tempTarget);
        transform.rotation = Quaternion.Euler(tempTarget);
    } 


    public void UpdateLocalRotation(Vector2 difference)  
    {
        difference.y *= -1;
        Vector3 temp = Vector3.Scale(difference, new Vector3(config.xDirection, config.yDirection, config.zDirection));
        temp = temp * config.sensitivity; 
        temp = transform.localEulerAngles + new Vector3(temp.y, temp.x, temp.z);
        temp = TestDomain(temp);
        transform.localRotation = Quaternion.Euler(temp);
    } 

    private Vector3 TestDomain(Vector3 value)
    {
        // test limits 
        if (config.xDomain)
        {
            float d = Mathf.DeltaAngle(0, value.y);
 
            if (config.xMin < d) value.y = config.xMin;
            else if (config.xMax > d) value.y = config.xMax;
        }
        if (config.yDomain)
        {
            float d = Mathf.DeltaAngle(0, value.x);

            if (config.yMin > d) value.x = config.yMin;
            else if (config.yMax < d) value.x = config.yMax;
        }
        if (config.zDomain)
        {
            float d = Mathf.DeltaAngle(0, value.z);

            if (config.zMin > d) value.z = config.zMin;            
            else if (config.zMax < d) value.z = config.zMax;
        }
        return value;
    }


}