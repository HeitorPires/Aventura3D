using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uIIcon;
        public TextMeshProUGUI uIValue;

        private ItemSetup _currSetup;

        public void Load(ItemSetup setup)
        {
            _currSetup = setup;
            UpdateUI();
        }

        public void UpdateUI()
        {
            uIIcon.sprite = _currSetup.icon;
        }

        private void Update()
        {
            uIValue.text = _currSetup.soInt.value.ToString();
        }

    }

}
