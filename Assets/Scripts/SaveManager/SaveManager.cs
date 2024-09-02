using System.IO;
using UnityEngine;
using Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using Cloth;

public class SaveManager : SingletonPersistent<SaveManager>
{
    public Action<SaveSetup> FileLoaded;

    private string _path = Application.dataPath + "/save.txt";
    public SaveSetup _saveSetup { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        Invoke(nameof(Load), .1f);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup
        {
            lastLevel = 0,
            playerName = ""
        };

    }

    #region SAVE
    [NaughtyAttributes.Button]
    public void HandleSave(int level, string name)
    {
        if (_saveSetup == null)
            CreateNewSave();
        SaveLastLevel(level);
        SaveName(name);
        SaveItems();
        SaveCheckpoints();
        SaveCloth();
        SaveLife();
        Save();
    }

    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup);
        SaveFile(setupToJson);
    }

    private void SaveFile(string json)
    {
        Debug.Log(_path);

        File.WriteAllText(_path, json);
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
        _saveSetup.lifePack = Items.ItemManager.Instance.GetByType(Items.ItemType.LIFE_PACK).soInt.value;
    }

    private void SaveCheckpoints()
    {
        List<CheckpointBase> checkpointList = CheckpointManager.Instance.checkpoints;
        checkpointList.ForEach(i =>
        {
            if(i.checkpointSaved && !_saveSetup.checkpoints.Contains(i.key))
            {
                _saveSetup.checkpoints.Add(i.key);
            }
        });
    }

    private void SaveCloth()
    {
        _saveSetup.currentCloth = (int)Player.Instance.CurrentCloth;
    }

    private void SaveLife()
    {
        _saveSetup.currentHealth = Player.Instance.healthBase.CurrentLife;
        if(_saveSetup.currentHealth <= 0)
        {
            _saveSetup.currentHealth = Player.Instance.healthBase.startLife;
        }
    }

    #endregion

    #region LOAD
    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);

            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);

            FileLoaded?.Invoke(_saveSetup);
        }
        else
        {
            CreateNewSave();
            Save();
        }
    }
    #endregion
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public int lifePack;
    public int currentCloth;
    public float currentHealth;
    public List<int> checkpoints = new();
}
