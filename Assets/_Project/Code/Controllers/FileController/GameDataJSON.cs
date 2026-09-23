using UnityEngine;

// T sould be [Serializable]
public class GameDataJSON<T>: IGameData<T>
{
    private T dataObj;

    public void SaveData(string data)
    {
        dataObj = JsonUtility.FromJson<T>(data);
    }

    public void SaveData(T data)
    {
        dataObj = data;
    }


    public T GetObject()
    {
        return dataObj;
    }

    public string GetData()
    {
        return JsonUtility.ToJson(dataObj);
    }
}
