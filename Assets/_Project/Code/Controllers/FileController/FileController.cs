using System.Diagnostics;
using System.IO;



// save system work now for editor mode
public static class FileController<T>
{

# if UNITY_EDITOR
    private static string pathToFile = "./Data/";
# endif

    // false => not exist file
    public static bool ReadFile(string nameFile, IGameData<T> objData)
    {
# if UNITY_EDITOR
        if (!Directory.Exists(pathToFile))
        {
            Directory.CreateDirectory(pathToFile);
            return false;
        }
        string content = File.ReadAllText(pathToFile + nameFile);
        if (content == "" || content == null) return false;
        objData.SaveData(content);
        UnityEngine.Debug.Log("File complete readed:: " + pathToFile + nameFile);
        return true;
# else
        return false;
# endif
    }

    public static void WriteFile(string nameFile, IGameData<T> objData)
    {
        # if UNITY_EDITOR
            // create folders if not exist
            if (!Directory.Exists(pathToFile))
            {
                Directory.CreateDirectory(pathToFile);
            }
            string content = objData.GetData();
            File.WriteAllText(pathToFile + nameFile, content);
            UnityEngine.Debug.Log("file writed to::" + pathToFile + nameFile);
        # endif
    }
}
