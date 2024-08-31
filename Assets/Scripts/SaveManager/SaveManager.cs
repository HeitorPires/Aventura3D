using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        _saveSetup = new SaveSetup
        {
            lastLevel = 2,
            playerName = "Heitor"
        };

    }

    #region SAVE
    [NaughtyAttributes.Button]

    public void HandleSave(int level, string name)
    {
        SaveLastLevel(level);
        SaveName(name);
        SaveItems();
        Save();
    }

    private void Save()
    {

        string setupToJson = JsonUtility.ToJson(_saveSetup);
        SaveFile(setupToJson);
    }

    private void SaveFile(string json)
    {
        string path = Application.dataPath + "/save.txt";
        Debug.Log(path);

        File.WriteAllText(path, json);
    }

    private void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
    }

    private void SaveName(string name)
    {
        _saveSetup.playerName = name;
    }

    private void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = Items.ItemManager.Instance.GetByType(Items.ItemType.LIFE_PACK).soInt.value;
    }

    #endregion


}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public int health;
}
