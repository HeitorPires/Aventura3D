using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemCoin : CollectableItemBase
{



    protected override void OnCollect()
    {
        base.OnCollect();

        ItemManager.Instance.AddCoins();
    }
}
