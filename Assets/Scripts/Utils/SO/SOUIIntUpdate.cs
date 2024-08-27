using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Items;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;
    public ItemManager itemManager;

    // Start is called before the first frame update
    void Awake()
    {
    }


    private void UpdateCoinsUI()
    {
        uiTextValue.text = $"X{soInt.value}";
    }
}
