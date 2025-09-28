using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [Header("Gun Basic SETUP")]
    public List<GunBase> gunList;
    public Transform gunPosition;
    public TextMeshProUGUI uiTextCurrentGun;
    public int initialGun = 0;

    private GunBase _currentGun;
    [SerializeField] private int _gunIndex = 0;

    protected override void Init()
    {
        base.Init();

        CreateGun(initialGun);

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            _gunIndex++;

            if (_gunIndex >= gunList.Count)
            {
                _gunIndex = 0;
            }

            CreateGun(_gunIndex);
        }
    }


    private void CreateGun(int gunToCreate)
    {
        _currentGun = Instantiate(gunList[gunToCreate], gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        uiTextCurrentGun.text = gunList[gunToCreate].tag.ToString();
        //Debug.Log("Current GUN: " + gunList[gunToCreate].tag);
    }
   
    private void StartShoot()
    {
        _currentGun.StartShoot();
        //Debug.Log("-> Start Shoot");
    }
    private void CancelShoot()
    {
        //Debug.Log("-> Cancel Shoot");
        _currentGun.StopShoot();
    }

}
