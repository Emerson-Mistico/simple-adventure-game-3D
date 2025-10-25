using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoins : ItemCollectableBase
{
    protected override void OnCollected()
    {
        base.OnCollected();
        ItemManager.Instance.AddItemCoins(itemValue);        
    }
}
