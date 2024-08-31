using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    [NaughtyAttributes.Button]
    private void Save()
    {
        SaveSetup setup = new SaveSetup();
        setup.lastLevel = 2;
        setup.playerName = "Heitor";

        string setupToJson = JsonUtility.ToJson(setup);
        SaveFile(setupToJson);
    }

    private void SaveFile(string json)
    {
        string path = Application.dataPath + "/save.txt";
        Debug.Log(path);

        File.WriteAllText(path, json);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
}
