using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
    {
    public class CollectableItemBase : MonoBehaviour
    {

        public ItemType itemType;

        public string playerTag = "Player";
        public ParticleSystem particleSystem;
        public float timeToDestroy = 3f;
        public GameObject graphicItem;

        [Header("Sounds")]
        public AudioSource audioSource;

        private bool _canCollect = true;
        
        private void OnTriggerEnter(Collider collision)
        {
            if (_canCollect && collision.gameObject.CompareTag(playerTag))
            {
                _canCollect = false;
                Collect();
            }
        }

        protected virtual void Collect()
        {
            OnCollect();
        }

        protected virtual void OnCollect()
        {
            ItemManager.Instance.AddByType(itemType);
            HideObject();
            PlayParticleSystem();
            PlaySounds();
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

        private void PlaySounds()
        {
            if (audioSource != null)
                audioSource.Play();
        }
    }

}
