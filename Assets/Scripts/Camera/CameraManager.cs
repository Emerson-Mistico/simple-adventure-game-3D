using UnityEngine;
using ESM.Core.Singleton;

[DefaultExecutionOrder(-999)]
public class CameraManager : Singleton<CameraManager>
{
    [Header("Activation Setup")]
    public GameObject cameraBossFight;
    
    private int _bossFightState = 0;

    protected override void Awake()
    {        
        base.Awake();

        PlayerPrefs.SetInt("BossFightState", 0);
    }

    private void Update()
    {
        _bossFightState = PlayerPrefs.GetInt("BossFightState");

        if (_bossFightState == 1)
        {
            BossFightCameraOn();
        }
        else 
        {
            BossFightCameraOff();
        }
    }

    private void BossFightCameraOn()
    {
        if (cameraBossFight != null)
        {
            cameraBossFight.SetActive(true);
        }
    }
    private void BossFightCameraOff()
    {
        if (cameraBossFight != null)
        {
            cameraBossFight.SetActive(false);
        }
    }

}
