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
        inputs.UI.ChangeGun.performed += cts => ChangeGun();

    }

    private void ChangeGun()
    {
        _gunIndex++;

        if (_gunIndex >= gunList.Count)
        {
            _gunIndex = 0;
        }

        CreateGun(_gunIndex);
    }
    private void CreateGun(int gunToCreate)
    {
        _currentGun = Instantiate(gunList[gunToCreate], gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;     

        uiTextCurrentGun.text = _currentGun.tag.ToString();        
        PlayerPrefs.SetString("CurrentGun", uiTextCurrentGun.text);
        Debug.Log("Current GUN now is -> " + PlayerPrefs.GetString("CurrentGun"));
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
