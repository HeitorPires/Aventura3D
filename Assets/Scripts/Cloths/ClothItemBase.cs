using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{

    public class ClothItemBase : MonoBehaviour
    {
        public ClothType type;
        public string tagToCompare = "Player";
        public float clothChangeDuration = 4f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(tagToCompare))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            var setup = ClothManager.Instance.GetSetupByType(type);
            Player.Instance.ChangeTexture(setup, clothChangeDuration);
            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

    }
}
