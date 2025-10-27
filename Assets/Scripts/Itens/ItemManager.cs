using ESM.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }
    public class ItemManager : Singleton<ItemManager>
    {
        [Header("Itens Collectable Setup")]
        public List<ItemSetup> itemSetups;

        private void Start()
        {
            ResetItens();
        }

        private void ResetItens()
        {
            foreach (var i in itemSetups)
            {
                i.SOInt.value = 0;
            }
        }
        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetups.Find(i => i.itemType == itemType);
        }
        public void AddItemByType(ItemType itemType, int amountValue)
        {
            itemSetups.Find(i => i.itemType == itemType).SOInt.value += amountValue;
        }
        public void RemoveItemByType(ItemType itemType, int amountValue)
        {
            if (itemSetups.Find(i => i.itemType == itemType).SOInt.value > 0) 
            {
                itemSetups.Find(i => i.itemType == itemType).SOInt.value -= amountValue;
            }            
        }

        #region DEBUG TEST
        [NaughtyAttributes.Button]
        public void TestAddCoin()
        {
            AddItemByType(ItemType.COIN, 1);
        }
        [NaughtyAttributes.Button]
        public void TestRemoveCoin()
        {
            RemoveItemByType(ItemType.COIN, 1);
        }
        [NaughtyAttributes.Button]
        public void TestAddLifePack()
        {
            AddItemByType(ItemType.LIFE_PACK, 1);
        }
        [NaughtyAttributes.Button]
        public void TestRemoveLifePack()
        {
            RemoveItemByType(ItemType.LIFE_PACK, 1);
        }
        #endregion

    }
    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt SOInt;
        public Sprite icon;
    }
}

