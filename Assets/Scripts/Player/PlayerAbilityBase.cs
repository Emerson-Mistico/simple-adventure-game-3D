using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityBase : MonoBehaviour
{
    protected PlayerManager player;

    protected Inputs_ESM inputs;

    private void OnValidate()
    {
        if (player == null)
        {
            player = GetComponent<PlayerManager>();
        }
    }

    private void Start()
    {
        inputs = new Inputs_ESM();
        inputs.Enable();
        
        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnEnable()
    {
        if(inputs != null)
        {
            inputs.Enable();
        }
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init() { }
    protected virtual void RegisterListeners() { }
    protected virtual void RemoveListeners() { }

}
