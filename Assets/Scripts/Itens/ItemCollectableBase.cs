using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Itens
{
    public class ItemCollectableBase : MonoBehaviour
    {
        [Header("Collect Setup")]
        public ItemType itemType;
        public string tagToCompare = "Player";
        public int itemValue = 1;

        public GameObject graphicItem;

        [Header("Animation")]
        public ParticleSystem particle;

        [Header("Sounds")]
        public AudioSource soundToUse;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(tagToCompare))
            {
                Collect();
            }
        }
        protected virtual void Collect()
        {
            SpriteRenderer iconRenderer = GetComponentInChildren<SpriteRenderer>();
            if (iconRenderer != null)
            {
                iconRenderer.enabled = false;
            }
            OnCollected();
        }
        protected virtual void OnCollected()
        {
            if (particle != null)
            {
                particle.Play();
                StartCoroutine(WaitAndDeactivate(particle.main.duration));
            }
            else
            {
                gameObject.SetActive(false);
            }
            if (soundToUse != null)
            {
                soundToUse.Play();
            }

            ItemManager.Instance.AddItemByType(itemType, itemValue);

        }
        private IEnumerator WaitAndDeactivate(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}