using ESM.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager : Singleton<ItemManager>
{
    [Header("Itens Collectable Setup")]
    public SOInt itemCoins;
    public SOInt itemEnergy;

    [Header("Player Item Init Setup")]
    public int initialCoins = 0;
    public int initialEnergy = 100;

    private void Start()
    {
        ResetItens();
    }

    private void ResetItens()
    {
        itemCoins.value = initialCoins;
        itemEnergy.value = initialEnergy;
    }

    public void AddItemCoins(int amountCoins) {

        itemCoins.value += amountCoins;
        Debug.Log("Add Coin -> " + amountCoins + " / Total Coins -> " + itemCoins.value);
    }

    public void AddItemEnergy(int amountEnergy)
    {

        itemEnergy.value += amountEnergy;
    }

    public void RemoveItemEnergy(int amountEnergy)
    {

        itemEnergy.value -= amountEnergy;
    }

}
