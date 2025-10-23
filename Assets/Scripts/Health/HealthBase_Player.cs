using UnityEngine;
using System;
using System.Collections.Generic;

public class HealthBase_Player : MonoBehaviour, IDamageable
{
    [Header("Life Settings")]
    public float startLife = 10f;
    public bool destroyOnKill = false;

    [SerializeField] private float _currentLife;

    public Action<HealthBase_Player> OnDamage;
    public Action<HealthBase_Player> OnKill;

    public List<UIEssentialUpdater> uiEssentialUpdater;

    private void Awake()
    {
        Init();
    }

    private void Init() 
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        PlayerPrefs.SetInt("PlayerIsAlive", 1);
        PlayerPrefs.SetInt("BossFightState", 0);
        UpdateUI();
    }

    protected virtual void Kill() 
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 3f);
        }
        OnKill?.Invoke(this);
    }

    public void Damage(float f)
    {
        _currentLife -= f;
        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 direction)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiEssentialUpdater != null)
        {
            // Debug.Log("Player Life -> " + _currentLife + " / " + startLife);
            uiEssentialUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

}
