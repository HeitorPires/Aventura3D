using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class CollectableItemBase : MonoBehaviour
    {
        public SFXType sFXType = SFXType.NONE;

        public ItemType itemType;

        public string playerTag = "Player";
        public ParticleSystem particleSystem;
        public float timeToDestroy = 3f;
        public GameObject graphicItem;

        private bool _canCollect = true;

        private void OnTriggerEnter(Collider collision)
        {
            if (_canCollect && collision.gameObject.CompareTag(playerTag))
            {
                _canCollect = false;
                Collect();
            }
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sFXType);
        }

        protected virtual void Collect()
        {
            OnCollect();
        }

        protected virtual void OnCollect()
        {
            ItemManager.Instance.AddByType(itemType);
            PlaySFX();
            HideObject();
            PlayParticleSystem();
            Destroy(gameObject, timeToDestroy);
        }


        private void HideObject()
        {
            if (graphicItem != null)
                graphicItem.gameObject.SetActive(false);
        }

        private void PlayParticleSystem()
        {

            if (particleSystem != null)
                particleSystem.Play();

        }
    }

}
