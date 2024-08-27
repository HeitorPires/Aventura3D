using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{

    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabItemLayout;
        public Transform container;

        public List<ItemLayout> itemLayouts;

        private void Start()
        {
            CreateItems();
        }

        private void CreateItems()
        {
            foreach (var setup in ItemManager.Instance.itensSetups)
            {
                var item = Instantiate(prefabItemLayout, container);
                item.Load(setup);
                itemLayouts.Add(item);
            }
        }
    }
}
