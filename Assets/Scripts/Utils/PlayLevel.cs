using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }

    private void OnLoad(SaveSetup setup)
    {
        uiTextName.text = "Play " + (setup.lastLevel + 1);
    }

    private void OnDestroy()
    {
        
        SaveManager.Instance.FileLoaded -= OnLoad;
    }

}
