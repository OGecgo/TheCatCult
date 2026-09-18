using UnityEngine;

public interface  IGameData<T>
{
    public void SaveData(string data);
    public void SaveData(T data);

    public string GetData();
    public T GetObject();
}
