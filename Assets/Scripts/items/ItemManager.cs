using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using System;

namespace Items
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itensSetups;

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            itensSetups.ForEach(i => i.soInt.value = 0);
        }

        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;
            itensSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemovebyType(ItemType itemType, int amount = 1)
        {
            if(amount < 0) return;
            var item = itensSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if (item.soInt.value < 0)
                item.soInt.value = 0;


        }

        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }
        
        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

    }

    [Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }

}

