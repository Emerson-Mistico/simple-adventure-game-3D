using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected PlayerManager player;

    private void OnValidate()
    {
        if (player == null)
        {
            player = GetComponent<PlayerManager>();
        }
    }

    private void Start()
    {
        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init() { }
    protected virtual void RegisterListeners() { }
    protected virtual void RemoveListeners() { }

}
