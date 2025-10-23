using UnityEngine;
using ESM.Core.Singleton;
using Unity.Cinemachine;
using System.Collections;

public class CameraShakeManager : Singleton<CameraShakeManager>
{
    [Header("Shake Setup")]
    public CinemachineBasicMultiChannelPerlin cameraToShakeNormal;
    public CinemachineBasicMultiChannelPerlin cameraToShakeBossFight;
    public float normalShakeTime = 0.1f;
    public float bossShakeTime = 0.5f;

    #region PRIVATES
    private float _shakeTime;
    private Coroutine _currentShake;
    private int _currentBossFightState;
    private CinemachineBasicMultiChannelPerlin _actualCameratoShake;
    #endregion

    private void Start()
    {
        ShakeOffAll();
    }

    private void Update()
    {
        _currentBossFightState = PlayerPrefs.GetInt("BossFightState");

        if (_currentBossFightState == 0)
        {
            _actualCameratoShake = cameraToShakeNormal;
            _shakeTime = normalShakeTime;
        }
        else
        {
            _actualCameratoShake = cameraToShakeBossFight;
            _shakeTime = bossShakeTime;
        }
    }

    #region SKAKE CAMERAS
    public void ShakeON()
    {
        if (_currentShake != null)
            StopCoroutine(_currentShake);

        _currentShake = StartCoroutine(ShakeRoutine());
    }

    public void ShakeOFF()
    {
        if (_currentShake != null)
        {
            StopCoroutine(_currentShake);
            _currentShake = null;
        }
        _actualCameratoShake.enabled = false;
    }
    public void ShakeOffAll()
    {
        if (_currentShake != null)
        {
            StopCoroutine(_currentShake);
            _currentShake = null;
        }
        cameraToShakeNormal.enabled = false;
        cameraToShakeBossFight.enabled = false;
    }

    private IEnumerator ShakeRoutine()
    {
        _actualCameratoShake.enabled = true;
        yield return new WaitForSeconds(_shakeTime);
        _actualCameratoShake.enabled = false;
        _currentShake = null;
    }
    #endregion

    #region DEBUG
    [NaughtyAttributes.Button]
    public void TestShakeON()
    {
        ShakeON();
    }

    [NaughtyAttributes.Button]
    public void TestShakeOFF()
    {
        ShakeOFF();
    }
    #endregion
    
}
