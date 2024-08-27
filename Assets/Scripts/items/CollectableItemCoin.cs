using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class CollectableItemCoin : CollectableItemBase
{
    protected override void OnCollect()
    {
        base.OnCollect();

        ItemManager.Instance.AddByType(ItemType.COIN);
    }
}
