using Animation;
using Boss;
using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthBase : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    public float startLife = 10f;
    public bool destroyOnKill = false;

    [Header("Animation")]
    public FlashColor flashColor;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public UIBossUpdater uiBossUpdater;

    [SerializeField] private float _currentLife;
    [SerializeField] private string _currentTag;

    private void Awake()
    {
        Init();
        _currentTag = this.tag;
    }    

    protected virtual void Init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
        UiBossSetLife(_currentTag, _currentLife);
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 1.4f);
        }

        OnKill?.Invoke(this);
    }    

    [NaughtyAttributes.Button]
    public void DoSomeDamage()
    {
        if (_currentLife > 0)
        {
            Damage(5);
        }        
    }
    public void Damage(float damage, Vector3 direction)
    {
        if (flashColor != null)
        {
            flashColor.Flash();
        }

        transform.position -= transform.forward;

        _currentLife -= damage;

        UiBossUpdate(_currentTag, _currentLife, startLife);

        if (_currentLife <= 0)
        {
            Kill();
        }

        OnDamage?.Invoke(this);
    }

    public void Damage(float damage)
    {
        if (flashColor != null)
        {
            flashColor.Flash();
        }       

        _currentLife -= damage;

        UiBossUpdate(_currentTag, _currentLife, startLife);

        if (_currentLife <= 0)
        {
            Kill();
        }

        OnDamage?.Invoke(this);
    }

    private void UiBossUpdate(string tag, float cLife, float sLife)
    {
        if (tag == "Boss")
        {
            Debug.Log("É dano no Boss. Vida atual: " + cLife + "/" + sLife);
            uiBossUpdater.UpdateValue(sLife, cLife);
        }
    }
    private void UiBossSetLife(string tag, float sLife)
    {
        if (tag == "Boss")
        {
            Debug.Log("É dano no Boss. Vida atual: " + sLife + "/" + sLife);
            uiBossUpdater.UpdateValue(sLife);
        }
    }
}
