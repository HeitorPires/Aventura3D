using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using System;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public Action onCollect;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
        onCollect?.Invoke();
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        onCollect?.Invoke();
    }

}
