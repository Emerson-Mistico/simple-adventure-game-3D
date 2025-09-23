using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public InputAction shootAction;

    protected override void Init()
    {
        base.Init();
        shootAction.performed += ctx => Shoot();
    }

    private void OnEnable()
    {
        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable();
    }

    private void Shoot()
    {
        Debug.Log("-> Shoot");
    }


}
