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
        temp = TestMinMax(temp);
        Quaternion lookRotation = Quaternion.LookRotation(temp);  
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * sensitivity);
    } 
  
    public void UpdateLocalRotation(Vector2 difference)  
    {
        difference.y *= -1;
        // work with used direction
        Vector3 temp = Vector3.Scale(difference, onDirections);
        temp = temp * sensitivity;
        temp = TestMinMax(temp);
        transform.localEulerAngles = transform.localEulerAngles + new Vector3(temp.y, temp.x, temp.z);
    } 


    private Vector3 TestMinMax(Vector3 value)
    {
        // test limits 
        if (onMinMaxValues[0])
        {
            if (maxValues.x < value.x) value.x = maxValues.x;
            if (minValues.x > value.x) value.x = minValues.x;            
        }
        if (onMinMaxValues[1])
        {
            if (maxValues.y < value.y) value.y = maxValues.y;
            if (minValues.y > value.y) value.y = minValues.y;            
        }
        if (onMinMaxValues[2])
        {
            if (maxValues.z < value.z) value.z = maxValues.z;
            if (minValues.z > value.z) value.z = minValues.z;            
        }

        return value;

    }


}