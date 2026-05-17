using UnityEngine;

public class RotationControll : IRotationControll
{
    private Transform _transform;
    private float _sensitivity;
    private Vector3 _onDirections; // values = 0 or 1

    private bool[] _onMinMaxValues;
    private Vector3 _maxValues;
    private Vector3 _minValues;




    public float sensitivity { get { return _sensitivity; } set { _sensitivity = value; } }
    public Transform transform { get { return _transform; } set { _transform = value; } }
    public bool[] onMinMaxValues
    {
        get
        {
            if (_onMinMaxValues == null) _onMinMaxValues = new bool[3];
            return _onMinMaxValues;
        }
        set
        {
            if (value.Length != 3)
            {
                Debug.LogError("Class RotateControll:: Length of on_max_values is should be 3");
            }
            else _onMinMaxValues = value;
        }
    }
    public Vector3 maxValues { get { return _maxValues; } set { _maxValues = value; } }
    public Vector3 minValues { get {return _minValues; } set {_minValues = value; } }
    public Vector3 onDirections { get { return _onDirections; } set {_onDirections = value;} }


    public RotationControll(float sensitivity, Vector3 onDirections, Transform objTransform)
    {
        this.onMinMaxValues = new bool[3]; // all elements default to false
        this.onDirections = onDirections; 
        this.sensitivity = sensitivity;
        this.transform = objTransform;
    }


    public void UpdateRotateTo(Vector3 target) 
    { 
        Vector3 temp = target - transform.position;
        // work with used directions
        temp = Vector3.Scale(temp, onDirections); 
        Quaternion lookRotation = Quaternion.LookRotation(temp);  
        Quaternion temp_q = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * sensitivity);
        temp = temp_q.eulerAngles;
        temp = TestMinMax(temp);
        transform.rotation = Quaternion.Euler(temp);
    } 
  
    public void UpdateLocalRotation(Vector2 difference)  
    {
        difference.y *= -1;
        // work with used direction
        Vector3 temp = Vector3.Scale(difference, onDirections);
        temp = temp * sensitivity; 
        temp = transform.localEulerAngles + new Vector3(temp.y, temp.x, temp.z);
        temp = TestMinMax(temp);
        transform.localRotation = Quaternion.Euler(temp);
    } 
 

    private Vector3 TestMinMax(Vector3 value)
    {
        // test limits 
        if (onMinMaxValues[0]) // x direction
        {
            float d = Mathf.DeltaAngle(0, value.y);

            if (maxValues.x < d) value.y = maxValues.x;
            if (minValues.x > d) value.y = minValues.x;
        }
        if (onMinMaxValues[1]) // y direction
        {
            float d = Mathf.DeltaAngle(0, value.x);

            if (minValues.y > d) value.x = minValues.y;
            else if (maxValues.y < d) value.x = maxValues.y;
        }
        if (onMinMaxValues[2]) // z direction
        {
            float d = Mathf.DeltaAngle(0, value.z);

            if (minValues.z > d) value.z = minValues.z;            
            else if (maxValues.z < d) value.z = maxValues.z;
        }
        return value;
    }


}