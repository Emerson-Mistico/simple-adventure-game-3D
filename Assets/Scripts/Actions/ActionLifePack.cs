using Itens;
using UnityEngine;

public class ActionLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.M;
    public SOInt itemAmount;

    private void Start()
    {
        itemAmount = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).SOInt;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoveryLife();
        }
    }

    private void RecoveryLife()
    {
        var _currentLife = PlayerManager.Instance.playerHealth.LifeIsFull();
        if (itemAmount.value > 0 && _currentLife == false)
        {
            ItemManager.Instance.RemoveItemByType(ItemType.LIFE_PACK, 1);
            PlayerManager.Instance.playerHealth.ResetLife();
        }
    }

    #region DEBUG 
    [NaughtyAttributes.Button]
    public void LifeRestore()
    {
        RecoveryLife();
    }
    #endregion
}
